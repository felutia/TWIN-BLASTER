
/*!
 *  @file       enemy.cs
 *  @brief      敵ベース
 *  @author     Riki Ito
 *  @date       2014/10/16
 *  @since      0.1.0
 *
 *  Copyright (c) 2014, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;
using System.Collections.Generic;

namespace game
{
    /// <summary>
    /// 敵クラス
    /// </summary>
    public abstract class IEnemy
		: IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IEnemy( Vector2 pos, float speed, int power, int life, INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id )
			: base( )
		{
			// 位置設定
			m_Pos				= pos;

			// 速度
			m_Speed				= speed;
			
			// 威力
			m_Power				= power;
			
			// 体力
			m_Life				= life;
			
			// 最大体力
			m_max_life 			= m_Life;
			
			// サイズ生成
			m_Size 				= new aqua.CSize( );
			
			// 状態
			m_State = STATE.INSTAGE;
			
			// 登場状態
			m_InStagePetten = instage_id;
			
			// 行動状態
			m_OutStagePattern = outstage_id;
			
			StageLv = CStageLv.GetInstance( ).StageLv;
		}
			
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			switch (　m_State　) 
			{
				case STATE.INSTAGE:
				{
					// 登場
					switch ( m_InStagePetten )
					{
						case INSTAGE_ID.INSTAGE_FRONT:	{ InStage_Front(); } break;	// 前方(左)へ移
						case INSTAGE_ID.INSTAGE_BACK:	{ InStage_Back();  } break;	// 後方(右)へ移動
						case INSTAGE_ID.INSTAGE_UP:		{ InStage_Up();    } break;	// 上昇(上)へ移動
						case INSTAGE_ID.INSTAGE_DOWN:	{ InStage_Down();  } break;	// 下降(下)へ移動
						case INSTAGE_ID.INSTAGE_CICLE:	{ InStage_Cicle(); } break;	// 回転しながら移動
					}
				
					// 自機弾との当たり判定
					EnemyToPlayerBullet();
				}
				break;
				case STATE.OUTSTAGE:
				{
					// 退場
					switch (　m_OutStagePattern　) 
					{
						case OUTSTAGE_ID.OUTSTAGE_FRONT:	{ OutStage_Front();}		break;	// 前方(左)へ移動
						case OUTSTAGE_ID.OUTSTAGE_BACK:		{ OutStage_Back(); }		break;	// 後方(右)へ移動
						case OUTSTAGE_ID.OUTSTAGE_UP:		{ OutStage_Up();   }		break;	// 上昇(上)へ移動
						case OUTSTAGE_ID.OUTSTAGE_DOWN:		{ OutStage_Down(); }		break;	// 下降(下)へ移動
						case OUTSTAGE_ID.OUTSTAGE_CICLE:	{ OutStage_Cicle();}		break;	// 回転しながら移動
					}
				
					// 自機弾との当たり判定
					EnemyToPlayerBullet();
				}
				break;
				case STATE.ATTACK:
				{
					// 攻撃
					Attack( );
				
					// 自機弾との当たり判定
					EnemyToPlayerBullet();
				}
				break;
				case STATE.DEAD:
				{
					Dead();
				}
				break;
			}
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose()
		{
			// スプライト解放
			m_Sprite.Dispose( );
		}
		
		/*
		/// <summary>
		/// 登場
		/// </summary>
		public virtual void InStage()
		{
			switch( m_InStagePetten )
			{
				case INSTAGE_ID.INSTAGE_FRONT:		InStage_Front( );	break;	// 前進
				case INSTAGE_ID.INSTAGE_BACK:		InStage_Back( );	break;	// 後進
				case INSTAGE_ID.INSTAGE_UP:			InStage_Up( );		break;	// 上昇
				case INSTAGE_ID.INSTAGE_DOWN:		InStage_Down( );	break;	// 下降
			}
		}
		*/
		
		/// <summary>
		/// 攻撃
		/// </summary>
		public void Attack()
		{
			switch ( m_AttackPattern ) 
			{
				case ATTACK_PATTERN.ATTACK_NORMAL:	AttackNormal(); break;	// 通常攻撃
			}
		}
		
		/// <summary>
		/// 死亡
		/// </summary>
		public void Dead()
		{
			// SE
			CSoundManager.GetInstance().Play( SOUND_ID.DAMEGE, 0.3f);
			
			m_IsActive = false;
		}
		
		/// <summary>
		/// 被弾
		/// </summary>
		public void BeShot( int damage )
		{
			m_Life -= damage;
			
			// ヒットエフェクト
			CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
			CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
			CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
			CreateEffect( EFFECT_ID.HIT_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );

			if( m_Life <= 0 )
			{
				// 死亡エフェクト
				CreateEffect( EFFECT_ID.DEAD_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
				
				// 死亡SE
				
				m_State = STATE.DEAD;
			}
		}
		
		/// <summary>
		/// 通常攻撃
		/// </summary>
		public virtual void AttackNormal(){}
		
		/// <summary>
		/// 登場パターン
		/// </summary>
		public virtual void InStage_Front()
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
            
            // ゆっくり左へ移動
            m_Pos.X -= m_Speed;
			
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
			
			// 移動しおわったら
			if( Movement > 100 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public virtual void InStage_Back()
		{
         	// 現在の位置を設定
            m_Pos = m_Sprite.Position;
            
            // ゆっくり右へ移動
            m_Pos.X += m_Speed;
            
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
			
			// 移動しおわったら
			if( Movement > 100 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public virtual void InStage_Up()
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
            
            // ゆっくり上へ移動
            m_Pos.Y -= m_Speed;
            
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
			
			// 移動しおわったら
			if( Movement > 100 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public virtual void InStage_Down()
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
            
            // ゆっくり左へ移動
            m_Pos.Y += m_Speed;
            
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
			
			// 移動しおわったら
			if( Movement > 100 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public virtual void InStage_Cicle( )
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
			
			m_angle += FMath.PI / 60;
			
			m_rSpeed.X = ((float)rx + 60) * FMath.Cos( m_angle ) * 0.1f;
			m_rSpeed.Y = ((float)ry + 60) * FMath.Sin( m_angle ) * 0.1f;
			
			rx -= 0.2f;
			ry -= 0.2f;
			
			m_Pos -= m_rSpeed;
			
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
		
			// 移動しおわったら
			if( Movement > 1400 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		/// <summary>
		/// 行動パターン
		/// </summary>
		public virtual void OutStage_Front( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり左へ移動
	        m_Pos.X -= m_Speed;
			
			// アクティブフラグOFF
			if( m_Pos.X < 0.0f - 32.0f || m_Pos.X > 960.0f + 32.0f ||
				m_Pos.Y < 128.0f - 32.0f || m_Pos.Y > 544.0f + 32.0f )
			{
				m_IsActive = false;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public virtual void OutStage_Back( )
		{
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり右へ移動
	        m_Pos.X += m_Speed;
			
			// アクティブフラグOFF
			if( m_Pos.X < 0.0f - 32.0f || m_Pos.X > 960.0f + 32.0f ||
				m_Pos.Y < 128.0f - 32.0f || m_Pos.Y > 544.0f + 32.0f )
			{
				m_IsActive = false;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public virtual void OutStage_Up( )
		{
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり上へ移動
	        m_Pos.Y -= m_Speed;
			
			// アクティブフラグOFF
			if( m_Pos.X < 0.0f - 32.0f || m_Pos.X > 960.0f + 32.0f ||
				m_Pos.Y < 128.0f - 32.0f || m_Pos.Y > 544.0f + 32.0f )
			{
				m_IsActive = false;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public virtual void OutStage_Down( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり下へ移動
	        m_Pos.Y += m_Speed;     
			
			// アクティブフラグOFF
			if( m_Pos.X < 0.0f - 32.0f || m_Pos.X > 960.0f + 32.0f ||
				m_Pos.Y < 128.0f - 32.0f || m_Pos.Y > 544.0f + 32.0f )
			{
				m_IsActive = false;
			} 
			
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public virtual void OutStage_Cicle( )
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
			
			m_angle += FMath.PI / 60;
			
			m_rSpeed.X = (float)rx * FMath.Cos( m_angle ) * 0.1f;
			m_rSpeed.Y = (float)ry * FMath.Sin( m_angle ) * 0.1f;
			
			rx += 0.5f;
			ry += 0.5f;
			
			m_Pos += m_rSpeed;
			
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
		
			// アクティブフラグOFF
			if( m_Pos.X < 0.0f - 32.0f || m_Pos.X > 960.0f + 32.0f ||
				m_Pos.Y < 128.0f - 32.0f || m_Pos.Y > 544.0f + 32.0f )
			{
				m_IsActive = false;
			} 
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
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
        public void CreateBullet( ENEMY_BULLET_ID id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
        {
            CEnemyBulletManager.GetInstance( ).Create( id, OBJECT_ID.ENEMY_BULLET, color, reflec, m_Pos + m_Sprite.Center + pos, direction, speed, power );
        }
		
		/// <summary>
		/// 敵と自機弾との当たり判定
		/// </summary>
		public void EnemyToPlayerBullet( )
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			for (int i = 0; i < list.Count; ++i)
			{
				// 敵機弾と比較
				if( list[i].ObjectID == OBJECT_ID.PLAYER_BULLET )
				{
					IPlayerBullet pbullet = (IPlayerBullet)list[i];
					
					if( pbullet == null )
						return;
					
					// 弾のサイズ取得
					aqua.CSize size = new aqua.CSize( );
					
					size.width  = pbullet.Size.width  * 0.5f;
					size.height = pbullet.Size.height * 0.5f;
					
					Vector2 ev = m_Sprite.Position + m_Sprite.Center;
					Vector2 bv = new Vector2( pbullet.Sprite.Position.X + size.width, pbullet.Sprite.Position.Y + size.height );
					
					Vector2 v = ev - bv;
					
					// 距離算出
					float len = v.Length( );
					
					// 当たっていたら
					if( m_EnemyID == ENEMY_ID.ENEMY_BLUE || m_EnemyID == ENEMY_ID.ENEMY_RED )
					{
						if( len < 16.0f )
						{
							BeShot( pbullet.Power );
							
							// 弾解放
							pbullet.Active = false;
							
							break;
						}
					}
					else if( m_EnemyID == ENEMY_ID.ENEMY_BLUE_LV2 || m_EnemyID == ENEMY_ID.ENEMY_RED_LV2 )
					{
						if( len < 32.0f )
						{
							BeShot( pbullet.Power );
							
							// 弾解放
							pbullet.Active = false;
							
							break;
						}
					}
				}
			}
		}
		
        /// <summary>
        /// スプライトの取得
        /// </summary>
        /// <value>
        /// スプライト
        /// </value>
        public aqua.CSprite Sprite
        {
            get { return m_Sprite; }
        }
        
        /// <summary>
        /// サイズの取得
        /// </summary>
        /// <value>
        /// サイズ
        /// </value>
        public aqua.CSize Size
        {
            get { return m_Size; }
        }
        
        /// <summary>
        /// 威力取得
        /// </summary>
        /// <value>
        /// 威力
        /// </value>
        public int Power
        {
            get { return m_Power; }
        }
		
		/// <summary>
		/// 体力取得
		/// </summary>
		/// <value>
		/// 体力
		/// </value>
		public int Life
		{
			get { return m_Life; }
			set { m_Life = value;}
		}
		
		/// <summary>
		/// 状態ID
		/// </summary>
		public enum STATE
		{
			INSTAGE,	// 登場
			OUTSTAGE,	// 退場
			ATTACK,		// 攻撃
			DEAD,		// 死亡
		}
	
		/// <summary>
        /// 攻撃パターンID
        /// </summary>
        public enum ATTACK_PATTERN
		{
			ATTACK_NORMAL,		// 通常攻撃
		/*
			ATTACK_PATTERN_1,
			ATTACK_PATTERN_2,
			ATTACK_PATTERN_3,
			ATTACK_PATTERN_4,
			ATTACK_PATTERN_5,
			ATTACK_PATTERN_6,
			ATTACK_PATTERN_7,
		*/
        }
		
        /// <summary>
        /// スプライトクラス
        /// </summary>
        protected aqua.CSprite  		m_Sprite;
        
        /// <summary>
        /// 敵のサイズ
        /// </summary>
        protected aqua.CSize    		m_Size;
        
        /// <summary>
        /// 速度
        /// </summary>
        protected float         		m_Speed;
		
        /// <summary>
        /// 当たった時の威力
        /// </summary>
        protected int           		m_Power;
		
		/// <summary>
        /// 体力
        /// </summary>
        protected int					m_Life;
		
		/// <summary>
		/// 最大体力
		/// </summary>
		private int 					m_max_life;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2				m_Pos;
		
		/// <summary>
		/// 位置取得
		/// </summary>
		public Vector2 GetPos
		{
			get{ return m_Pos; }
		}
		
		/// <summary>
		/// 角度
		/// </summary>
		protected float 				m_angle;
		
		/// <summary>
		/// 移動量
		/// </summary>
		public int 						Movement;
		
		/// <summary>
		/// エネミーID
		/// </summary>
		public ENEMY_ID					m_EnemyID;
		
		/// <summary>
		/// 登場パターン
		/// </summary>
		public INSTAGE_ID				m_InStagePetten;
		
		/// <summary>
		/// 行動パターンID
		/// </summary>
		public OUTSTAGE_ID				m_OutStagePattern;
		
		/// <summary>
		/// 攻撃パターン
		/// </summary>
		public ATTACK_PATTERN			m_AttackPattern;
		
		/// <summary>
		/// エネミー数
		/// </summary>
		private const uint				m_enemy_num = 10;
		
		/// <summary>
		/// 敵の状態
		/// </summary>
		protected STATE					m_State;
		
		/// <summary>
		/// 移動パターン数
		/// </summary>
		protected const uint			m_move_num = 1;
		
		/// <summary>
		/// 攻撃回数
		/// </summary>
		protected int					m_attack_num;
		
		/// <summary>
		/// 攻撃間隔
		/// </summary>
		protected uint					m_attack_time;
		
		/// <summary>
		/// 敵弾生成間隔設定
		/// </summary>
		protected aqua.CFrameTimer		m_AttackTimer;
		
		//ステージレベル
		protected int					StageLv;
		
		/// 円運動用変数
		protected Vector2				m_rSpeed = new Vector2( 0.0f, 0.0f );
		protected float					m_degree = 120;
		protected double				rx, ry = 0;
    }
}
