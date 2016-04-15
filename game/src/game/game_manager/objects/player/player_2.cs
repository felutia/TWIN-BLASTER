/*!
 *  @file       player_2.cs
 *  @brief      自機2
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;
using System.Collections.Generic;
using Sce.PlayStation.Core.Input;

namespace game
{
	/// <summary>
	/// Player2クラス
	/// </summary>
	public class CPlayer2:IObject
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CPlayer2 ()
			: base()
		{
			m_ObjectID = OBJECT_ID.PLAYER2;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
			// プレイヤースプライト設定
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.PLAYER2 );
			m_Sprite.Position = new Vector2( 100.0f, 400.0f );
			m_Sprite.Center = new Vector2( 16.0f, 16.0f );
			
			m_GOinSprite = CSpriteManager.Create( TEXTURE_ID.GAMEOVER_IN );
			m_GOinSprite.Position = new Vector2( 200.0f , 250.0f );
			m_GOinSprite.Visible = false;
			
			m_NormalTimer = new aqua.CFrameTimer( 8 );
			m_VernierTimer = new aqua.CFrameTimer( 2 );
			m_InvincibleTimer = new aqua.CFrameTimer( m_InvincibleTime );
			m_BombTimer = new aqua.CFrameTimer( 0 );
			m_GameOverTimer = new aqua.CFrameTimer( 90 );
			
			// 通常状態設定
			m_State = STATE.NORMAL;
			
			m_Direction = 0.0f;
			
			m_Pos = new Vector2( 0.0f, 0.0f );
			m_Center = m_Sprite.Center;
			
			// 速度設定
			m_Speed = m_move_speed;
			
			m_AddAlpha = 0.06f;
			
			// 経験値
			m_EXP = 0;
			
			// 最大レベル
			m_max_Lv = 10;
			
			// 初期自機レベル
			m_Lv = 0;
			
			// 必要経験値
			m_need_exp = (int)((10 * ( Math.Pow(1.7f, m_Lv) - 1.7f ) / 0.7f) * 2);
			
			m_ResonantTimer = new aqua.CFrameTimer( 30 );
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			switch (m_State)
			{
			case STATE.NORMAL:
				{
					// 操作
					Control();
					
					m_Pos = m_Sprite.Position;
					
					// 移動計算
					m_Pos.X += FMath.Cos( m_Direction ) * m_Speed;
					m_Pos.Y += FMath.Sin( m_Direction ) * m_Speed;
					
					// 当たり判定
					PlayerToBullet( );
					PlayerToEnemy( );
					PlayerToPlayer( );
					
					// 位置再設定
					m_Sprite.Position = m_Pos;
					
					// 壁判定
					CheckArea();
				}
				break;
			case STATE.DEAD:
				{
					m_Lv = 0;
					m_EXP = 0;
					
					m_Sprite.Color.A = 0.0f;
					
					m_Pos = new Vector2( 0.0f, 0.0f );
					
					m_Sprite.Position = m_Pos;
					
					CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
					
					if( player1.P1State == CPlayer1.STATE.DEAD )
					{
						// ゲームオーバー
						m_State = STATE.GAMEOVER;
						
						player1.Active = false;
						
						m_GOinSprite.Visible = true;
					}
					else
					{
						// 残機が1以上になったら
						if( CUnit.GetInstance( ).Life > 0 )
						{
							CUnit.GetInstance( ).Life--;
							
							m_Sprite.Position = new Vector2( 100.0f, 400.0f );
							
							m_AddAlpha = 0.06f;
							
							m_TwinBlaster = true;
							
							m_State = STATE.INVINCIBLE;
						}
					}
				}
				break;
			case STATE.INVINCIBLE:
				{
					// 無敵時間タイマー更新
					m_InvincibleTimer.Update();
					
					// 無敵時間中は(取敢えず)自機を半透明にして点滅させる
					m_Sprite.Color.A += m_AddAlpha;
					
					if( m_Sprite.Color.A <= 0.0f )
						m_AddAlpha *= -1.0f;
					if( m_Sprite.Color.A > 1.0f )
						m_AddAlpha *= -1.0f;
					
					// 通常操作は可能
					// ダメージ判定無
					Control();
					
					// 当たり判定(自機との当たり判定のみ)
					PlayerToPlayer( );
					
					m_Pos = m_Sprite.Position;
					
					// 移動計算
					m_Pos.X += FMath.Cos( m_Direction ) * m_Speed;
					m_Pos.Y += FMath.Sin( m_Direction ) * m_Speed;
					
					// 位置再設定
					m_Sprite.Position = m_Pos;
					
					// 壁判定
					CheckArea();
					
					// 時間切れで無敵時間終了
					if( m_InvincibleTimer.IsEnd( ) == true )
					{
						m_State = STATE.NORMAL;
					
						m_Sprite.Color.A = 1.0f;
					}
				}
				break;
			case STATE.GAMEOVER:
				{
					m_GameOverTimer.Update( );
					
					m_AddAlpha = 0.05f;
					
					m_GOinSprite.Color.A += m_AddAlpha;
					
					if( m_GOinSprite.Color.A >= 1.0f )
					{
						m_GOinSprite.Color.A = 1.0f;
					}
					
					if( m_GameOverTimer.IsEnd( ) == true )
					{
						m_IsActive = false;
					
						CStageLv.GetInstance( ).StageLv--;
						
						// リザルトへ移行
						CSceneManager.GetInstance().ChangeScene( SCENE_ID.RESULT );
					}
				}
				break;
			}
		}
		
		/// <summary>
		/// 操作処理
		/// </summary>
		public void Control()
		{
			m_Speed = 0.0f;
			
#if true
			Vector2 d = aqua.CGamePad.RightAnalogStick( );
			float abs = FMath.Abs( d.Length( ) );
			
			if(  abs > 0.0f )
			{
				m_Direction = FMath.Atan2( d.Y, d.X );
				
				m_Speed = m_move_speed;
				
				// R低速移動
				if( aqua.CGamePad.Button( GamePadButtons.R ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
#else
			// 左
			if( aqua.CGamePad.Button( GamePadButtons.Left ) == true )
			{
				m_Direction = FMath.Radians( 180.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 右
			if( aqua.CGamePad.Button( GamePadButtons.Right ) == true )
			{
				m_Direction = FMath.Radians( 0.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 上
			if( aqua.CGamePad.Button( GamePadButtons.Up ) == true )
			{
				m_Direction = FMath.Radians( 270.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 下
			if( aqua.CGamePad.Button( GamePadButtons.Down ) == true )
			{
				m_Direction = FMath.Radians( 90.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 左上
			if( aqua.CGamePad.Button( GamePadButtons.Left ) == true && aqua.CGamePad.Button( GamePadButtons.Up ) == true )
			{
				m_Direction = FMath.Radians( 225.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 右上
			if( aqua.CGamePad.Button( GamePadButtons.Right ) == true && aqua.CGamePad.Button( GamePadButtons.Up ) == true )
			{
				m_Direction = FMath.Radians( 315.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 左下
			if( aqua.CGamePad.Button( GamePadButtons.Left ) == true && aqua.CGamePad.Button( GamePadButtons.Down ) == true )
			{
				m_Direction = FMath.Radians( 135.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
			
			// 右下
			if( aqua.CGamePad.Button( GamePadButtons.Right ) == true && aqua.CGamePad.Button( GamePadButtons.Down ) == true )
			{
				m_Direction = FMath.Radians( 45.0f );
				
				m_Speed = m_move_speed;
				
				// L　低速移動
				if( aqua.CGamePad.Button( GamePadButtons.L ) == true )
				{
					m_Speed = m_move_speed / 3;
				}
			}
#endif
			// レーザーが出ていたら更新停止
			IEffect effect = (IEffect)CGameManager.GetInstance( ).FindObject( OBJECT_ID.EFFECT );
			
			if( effect != null )
			{
				if( effect.EffectID == EFFECT_ID.LASER_EFFECT || effect.EffectID == EFFECT_ID.CHARGE_EFFECT )
				{
					return;
				}
				else
				{
					// 通常弾用タイマー更新
					m_NormalTimer.Update( );
				}
			}
			
			// 攻撃(常に攻撃)
			if( m_NormalTimer.IsEnd( ) == true )
			{
				CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( 8.0f, -8.0f　), 10.0f, 25.0f, 2 );
				CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( 8.0f, -8.0f　),-10.0f, 25.0f, 2 );
				
				if( m_Lv >= 3 )
				{
					CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( 8.0f, -8.0f　), 0.0f, 30.0f, 3 );
					
					// レベル3
					if( m_Lv >= 7 )
					{
						CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( 8.0f, -8.0f　),  20.0f, 20.0f, 4 );
						CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( 8.0f, -8.0f　), -20.0f, 20.0f, 4 );
					
						// レベル4
						if( m_Lv >= 10 )
						{
							CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( -10.0f,-36.0f　), 0.0f, 20.0f, 4 );
							CreateBullet( PLAYER_BULLET_ID.INITIA_BULLET, new Vector2( -10.0f, 18.0f　), 0.0f, 20.0f, 4 );
						}
					}
				}
			}
			
			// ボム(マスパ(敵弾消滅))
			switch( m_bomb_state )
			{
				case BOMB_STATE.WAIT:
					{
						if( aqua.CGamePad.Button( GamePadButtons.Left) && aqua.CGamePad.Button( GamePadButtons.Circle) )
						{
							if( CUnit.GetInstance( ).Bom >= 4 && m_TwinBlaster == true && m_ResonantFlag == true )
							{
								if( effect != null )
								{
									if( effect.EffectID == EFFECT_ID.LASER_EFFECT || effect.EffectID == EFFECT_ID.CHARGE_EFFECT )
										break;
									else
									{
										if( aqua.CGamePad.Button( GamePadButtons.Left) || aqua.CGamePad.Button( GamePadButtons.Circle) )
										{
											m_bomb_state = BOMB_STATE.TWINBLASTER;
											
											break;
										}
									}
								}
							}
						}
						else if( aqua.CGamePad.Released( GamePadButtons.Circle ) && CUnit.GetInstance( ).Bom != 0 && m_TwinBlaster == true )
						{
							if( effect != null )
							{
								if( effect.EffectID == EFFECT_ID.LASER_EFFECT || effect.EffectID == EFFECT_ID.CHARGE_EFFECT )
									break;
								else
								{
									CSoundManager.GetInstance( ).Play( SOUND_ID.BLUE_BOM, 0.7f );
									
									CUnit.GetInstance( ).Bom--;
									
									m_bomb_state = BOMB_STATE.CREATE;
									
									break;
								}
							}
						}
					}
					break;
				case BOMB_STATE.CREATE:
					{
						CreateBomb( );
					}
					break;
				case BOMB_STATE.TWINBLASTER:
					{
						CreateTwinBlaster( );
					}
					break;
			}
			
			// バーニア生成タイマー更新
			m_VernierTimer.Update( );
			
			if( m_VernierTimer.IsEnd( ) == true )
			{
				// バーニアエフェクト生成
				CreateEffect( EFFECT_ID.VERNIER_EFFECT, COLOR_ID.RAINBOW, m_Pos );
			}
		}
		
		/// <summary>
		/// 弾の作成
		/// </summary>
		/// <param name='type'>
		/// Type.
		/// </param>
		/// <param name='pos'>
		/// Position.
		/// </param>
		/// <param name='angle'>
		/// Angle.
		/// </param>
		/// <param name='speed'>
		/// Speed.
		/// </param>
		/// <param name='power'>
		/// Power.
		/// </param>
		public void CreateBullet( PLAYER_BULLET_ID id, Vector2 pos, float direction, float speed, int power )
		{
			CPlayerBulletManager.GetInstance( ).Create( id, OBJECT_ID.PLAYER_BULLET, m_Pos + m_Sprite.Center + pos, direction, speed, power );
		}
		
		/// <summary>
		/// ボム生成
		/// </summary>
		public void CreateBomb( )
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			for (int i = 0; i < list.Count; ++i)
			{
				// 敵機弾と比較
				if( list[i].ObjectID == OBJECT_ID.ENEMY_BULLET )
				{
					for( ; ; )
					{
						IEnemyBullet enemybullet = (IEnemyBullet)list[i];
						
						// 弾解放
						enemybullet.Active = false;
						
						CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, enemybullet.GetPos );
						
						break;
					}
				}
			}
			
			m_BombTimer.Update( );
			
			if( m_BombTimer.IsEnd( ) == true )
			{
				m_BombTimer = new aqua.CFrameTimer( 3 );
				
				// ランダム生成
				int x    = aqua.CRandom.GetInstance( ).GetValue( 900 - 32 )  + 32;	// X座標
				int y    = aqua.CRandom.GetInstance( ).GetValue( 544 - 172 ) + 32;	// Y座標
				Vector2 create = new Vector2( x, y );
				
				CreateEffect( EFFECT_ID.MASUPA_EFFECT, COLOR_ID.RAINBOW, create );
				
				m_createcount++;
				
				if( m_createcount == 20 )
				{
					m_createcount = 0;
					m_BombTimer = new aqua.CFrameTimer( 0 );
					m_bomb_state = BOMB_STATE.WAIT;
				}
			}
		}
		
		/// <summary>
		/// ツインブラスター生成
		/// </summary>
		public void CreateTwinBlaster(  )
		{
			CreateEffect( EFFECT_ID.CHARGE_EFFECT, COLOR_ID.BLUE, m_Pos + new Vector2( 5.0f, -23.0f ) );
			
			CSoundManager.GetInstance( ).Play( SOUND_ID.LASER, 1.0f );
			
			m_bomb_state = BOMB_STATE.WAIT;
			
			// ツインブラスターフラグOFF
			m_TwinBlaster = false;
		}
		
		/// <summary>
		/// エフェクトの作成
		/// </summary>
		/// <param name='type'>
		/// Type.
		/// </param>
		/// <param name='pos'>
		/// Position.
		/// </param>
		/// <param name='angle'>
		/// Angle.
		/// </param>
		/// <param name='speed'>
		/// Speed.
		/// </param>
		/// <param name='power'>
		/// Power.
		/// </param>
		public void CreateEffect( EFFECT_ID id, COLOR_ID color, Vector2 pos )
		{
			CEffectManager.GetInstance( ).Create( id, OBJECT_ID.EFFECT, color, pos );
		}
		
        /// <summary>
        /// 画面内移動チェック
        /// </summary>
        public void CheckArea( )
		{
			// 現在の位置を取得
			m_Pos = m_Sprite.Position;
			
			// 左壁
			if( m_Pos.X < 0.0f )
				m_Pos.X = 0.0f;
			
			// 右壁
			if( m_Pos.X > 960.0f - 32.0f )
				m_Pos.X = 960.0f - 32.0f;
			
			// 上壁
			if( m_Pos.Y < 128.0f )
				m_Pos.Y = 128.0f;
			
			// 下壁
			if( m_Pos.Y > 544.0f - 32.0f )
				m_Pos.Y = 544.0f - 32.0f;
			
			// 位置を再設定
			m_Sprite.Position = m_Pos;
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose ()
		{
			m_Sprite.Dispose();
			m_GOinSprite.Dispose( );
		}
		
		/// <summary>
		/// 消滅
		/// </summary>
		public void Dead()
		{
			CSceneManager.GetInstance().ChangeScene(SCENE_ID.TITLE);
		}
		
		/// <summary>
		/// 自機と敵弾との当たり判定
		/// </summary>
		public void PlayerToBullet()
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			for (int i = 0; i < list.Count; ++i)
			{
				// 敵機弾と比較
				if( list[i].ObjectID == OBJECT_ID.ENEMY_BULLET )
				{
					IEnemyBullet ebullet = (IEnemyBullet)list[i];
					
					// 弾のサイズ取得
					aqua.CSize size = new aqua.CSize( );
					
					size.width  = ebullet.Size.width  * 0.5f;
					size.height = ebullet.Size.height * 0.5f;
					
					Vector2 pv = m_Sprite.Position + m_Sprite.Center / 2;
					Vector2 bv = new Vector2( ebullet.Sprite.Position.X + size.width , ebullet.Sprite.Position.Y + size.height );
					
					Vector2 v = pv - bv;
					
					// 距離算出
					float len = v.Length( );
					
					// 敵弾の色を取得
					COLOR_ID bColor = ebullet.Color;
					float range = 0.0f;
					
					// 赤弾と白弾の時
					if( bColor == COLOR_ID.RED || bColor == COLOR_ID.WHITE )
						range = 12.0f;
					// 青弾の時
					else if( bColor == COLOR_ID.BLUE )
						range = 24.0f;
					
					if( len < range )
					{
						// 赤弾と白弾の時ダメージ
						if( bColor == COLOR_ID.RED || bColor == COLOR_ID.WHITE )
						{
							// ヒットエフェクト
							CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
							
							BeShot( 1 );
						}
						// 青弾の時吸収エフェクト
						else if( bColor == COLOR_ID.BLUE )
						{
							// 吸収エフェクト
							CreateEffect( EFFECT_ID.REPEL_EFFECT, COLOR_ID.BLUE, m_Pos + m_Sprite.Center );
							CSoundManager.GetInstance( ).Play( SOUND_ID.DRAIN, 1.0f );
							
							// 経験値加算(自機同士が接近していた場合経験値加算2倍)
							CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER1);
							
							if( player1 == null )
								return;
							
							Vector2 playerv = ( m_Pos + m_Center ) - ( player1.Position + player1.Center );
							
							float playerLen = playerv.Length( );
							
							if( playerLen < 64.0f )
								m_EXP += 4;
							else
								m_EXP += 2;
							
							// 経験値計算
							m_need_exp = (int)((10 * ( Math.Pow(1.7f, m_Lv) - 1.7f ) / 0.7f) * 2);
							
							// 現在レベルの必要経験値(初期値*1.7の自機レベル乗)-1.7)/0.7) * 2 取得でLv.Up
							if( m_EXP >= m_need_exp )
							{
								m_Lv++;
								
								// レベルアップ字幕生成
								new CLvupBlueLogo( ).Initialize( );
//								CLogoManager.GetInstance().Create( LOGO_ID.LV_UP_BLUE );
								
								CUnit.GetInstance().Life++;
								CUnit.GetInstance().Bom++;
								
								// レベルアップ音再生
								CSoundManager.GetInstance( ).Play(SOUND_ID.LVUP, 0.7f);
								
								if( m_Lv != m_max_Lv )
									m_EXP = 0;
								else
								{
									m_Lv = m_max_Lv;
									
									m_EXP = m_need_exp;
								}
							}
						}
						
						// アクティブフラグ
						ebullet.Active = false;
						
						break;
					}
				}
			}
		}
		
		/// <summary>
		/// 自機と敵との当たり判定
		/// </summary>
		public void PlayerToEnemy()
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			for (int i = 0; i < list.Count; ++i)
			{
				// 敵と比較
				if( list[i].ObjectID == OBJECT_ID.ENEMY )
				{
					IEnemy enemy = (IEnemy)list[i];
					
					// 敵のサイズ取得
					aqua.CSize size = new aqua.CSize( );
					
					size.width  = enemy.Size.width  * 0.5f;
					size.height = enemy.Size.height * 0.5f;
					
					Vector2 pv = m_Sprite.Position + m_Sprite.Center / 2;
					Vector2 ev = new Vector2( enemy.Sprite.Position.X + size.width, enemy.Sprite.Position.Y + size.height );
					
					Vector2 v = pv - ev;
					
					// 距離算出
					float len = v.Length( );
					
					if( len < size.width )
					{
						// 仮ダメージ
						BeShot( enemy.Power );
						
						break;
					}
				}
			}
		}
	
#if true
		/// <summary>
		/// 自機と自機の範囲判定
		/// </summary>
		public void PlayerToPlayer( )
		{
			// 自機①のサイズ取得
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER1 );
			
			if( player1 == null )
				return;
			
			Vector2 p2v = m_Sprite.Position + m_Sprite.Center;
			Vector2 p1v = player1.Position + player1.Center;
			
			Vector2 pv = p2v - p1v;
			
			// 距離算出
			float len = pv.Length( );
			
			if( len <= 64.0f && m_ResonantFlag == false )
			{
				// 共鳴エフェクト生成
				CreateEffect( EFFECT_ID.RESONANT_EFFECT, COLOR_ID.BLUE, m_Pos );
				
				// 共鳴SE生成
				CSoundManager.GetInstance( ).Play( SOUND_ID.RESONAT, 0.7f );
				
				m_ResonantFlag = true;
			}
			else if( len <= 64.0f && m_ResonantFlag == true )
			{
				m_ResonantTimer.Update( );
				
				if( m_ResonantTimer.IsEnd( ) == true )
				{
					// 共鳴SE生成
					CSoundManager.GetInstance( ).Play( SOUND_ID.RESONAT, 0.7f );
				}
			}
			
			if( len > 64.0f )
			{
				m_ResonantFlag = false;
			}
		}
#endif
		
		/// <summary>
		/// 被弾
		/// </summary>
		/// <returns>
		/// ダメージ
		/// </returns>
		/// <param name='damage'>
		/// Damage ダメージ
		/// </param>
		public void BeShot( int damage )
		{
			CUnit.GetInstance().Life -= damage;
			
			// しばらく無敵
			m_State = STATE.INVINCIBLE;
			
			if( CUnit.GetInstance().Life < 0 )
			{
				CUnit.GetInstance().Life = 0;
				
				// 死亡エフェクト
				CreateEffect( EFFECT_ID.DEAD_EFFECT, COLOR_ID.BLUE, m_Pos );
				
				// 死亡SE
				CSoundManager.GetInstance().Play(SOUND_ID.DAMEGE, 1.0f);
				
				// 死亡へ移行
				m_State = STATE.DEAD;
			}
		}
		
		/// <summary>
        /// 位置の取得と設定
        /// </summary>
        /// <value>
        /// 位置
        /// </value>
		public Vector2 Position
		{
			get { return m_Pos;  }
			set { m_Pos = value; }
		}
		
		/// <summary>
        /// 中心値の取得と設定
        /// </summary>
        /// <value>
        /// 中心値
        /// </value>
		public Vector2 Center
		{
			get { return m_Center;  }
			set { m_Center = value; }
		}
		
		/// <summary>
		/// 自機レベル取得
		/// </summary>
		/// <value>
		/// 自機レベル
		/// </value>
		public int Player2Lv
		{
			get{ return m_Lv; }
		}
		
		/// <summary>
		/// 経験値取得
		/// </summary>
		/// <value>
		/// 経験値
		/// </value>
		public int Player2Exp
		{
			get{ return m_EXP; }
		}
		
		/// <summary>
		/// 必要経験値取得
		/// </summary>
		/// <value>
		/// 必要経験値
		/// </value>
		public int NeedExp
		{
			get{ return m_need_exp; }
		}
		
		/// <summary>
		/// ボム状態取得
		/// </summary>
		/// <value>
		/// ボム状態
		/// </value>
		public BOMB_STATE BombState
		{
			get{ return m_bomb_state;}
		}
		
		/// <summary>
		/// 状態アクセッサ
		/// </summary>
		/// <value>
		/// 状態
		/// </value>
		public STATE P2State
		{
			get { return m_State; }
			set { m_State = value; }
		}
		
		/// <summary>
		/// ツインブラスター生成フラグ
		/// </summary>
		/// <value>
		/// フラグ
		/// </value>
		public bool TwinBlaster
		{
			get { return m_TwinBlaster; }
			set { m_TwinBlaster = value; }
		}
		
		/// <summary>
		/// 共鳴フラグ
		/// </summary>
		/// <value>
		/// フラグ
		/// </value>
		public bool Resonant
		{
			get { return m_ResonantFlag; }
			set { m_ResonantFlag = value; }
		}
		
        /// <summary>
        /// 状態ID
        /// </summary>
        public enum STATE
		{
			NORMAL,		// 通常
			DEAD,		// 死亡
			INVINCIBLE,	// 無敵
			GAMEOVER,	// ゲームオーバー
        }
		
		/// <summary>
		/// ボム生成状態
		/// </summary>
		public enum BOMB_STATE
		{
			  WAIT
			, CREATE
			, TWINBLASTER
		}
		
		/// <summary>
		/// スプライト
		/// </summary>
		private aqua.CSprite		m_Sprite;
		
		/// <summary>
		/// ゲームオーバースプライト
		/// </summary>
		private aqua.CSprite		m_GOinSprite;
		
		/// <summary>
		/// 状態
		/// </summary>
		private STATE				m_State;
		
		/// <summary>
		/// ボム生成状態
		/// </summary>
		private BOMB_STATE			m_bomb_state;
		
		/// <summary>
		/// 移動速度
		/// </summary>
		private const float			m_move_speed = 7.5f;
		
		/// <summary>
		/// 位置
		/// </summary>
		private Vector2				m_Pos;
				
		/// <summary>
		/// 中心値
		/// </summary>
		private Vector2				m_Center;
		
		/// <summary>
		/// 速度
		/// </summary>
		private float				m_Speed;
		
        /// <summary>
        /// 向き
        /// </summary>
        private float				m_Direction;
		
		/// <summary>
		/// 通常弾タイマー
		/// </summary>
		private aqua.CFrameTimer	m_NormalTimer;
		
		/// <summary>
		/// バーニア生成タイマー
		/// </summary>
		private aqua.CFrameTimer	m_VernierTimer;
		
		/// <summary>
		/// ボム生成タイマー
		/// </summary>
		private aqua.CFrameTimer	m_BombTimer;
		
		/// <summary>
		/// ゲームオーバータイマー
		/// </summary>
		private aqua.CFrameTimer	m_GameOverTimer;
		
		/// <summary>
		/// 無敵時間設定(3秒)
		/// </summary>
		private const int			m_InvincibleTime = 90;
		
		/// <summary>
		/// 無敵時間タイマー
		/// </summary>
		private aqua.CFrameTimer	m_InvincibleTimer;
		
		/// <summary>
		/// 加算するアルファ値
		/// </summary>
		private float				m_AddAlpha;
		
		/// <summary>
		/// 経験値
		/// </summary>
		private int					m_EXP;
		
		/// <summary>
		/// 最大レベル
		/// </summary>
		private int					m_max_Lv;
		
		/// <summary>
		/// レベル
		/// </summary>
		private int					m_Lv;
		
		/// <summary>
		/// 現在レベルの必要経験値
		/// </summary>
		private int					m_need_exp;
		
		/// <summary>
		/// ボム生成数
		/// </summary>
		private int 				m_createcount;
		
		/// <summary>
		/// 共鳴フラグ
		/// </summary>
		private bool				m_ResonantFlag = false;
		
		/// <summary>
		/// ツインブラスター発射準備
		/// </summary>
		private bool				m_TwinBlaster = true;
		
		/// <summary>
		/// 共鳴SE再生用タイマー
		/// </summary>
		private aqua.CFrameTimer	m_ResonantTimer;
	}
}