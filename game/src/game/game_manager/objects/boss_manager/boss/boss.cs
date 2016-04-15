
/*!
 *  @file       boss.cs
 *  @brief      ボス
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
    /// ボスクラス
    /// </summary>
    public abstract class IBoss
		: IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IBoss( Vector2 pos, float speed, int life )
			: base()
		{
			// 位置設定
			m_Pos		= pos;
			
			// 速度
			m_Speed		= speed;
			
			// 体力
			m_Life		= life;
			
			// 最大体力
			m_max_life 	= m_Life;
			
			// 状態
			m_State		= STATE.INSTAGE;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override　void Update()
		{
			switch (　m_State　) 
			{
			case STATE.INSTAGE:
				{
					m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
					
					// 登場
					InStage( );
				}
				break;
			case STATE.ACTION:
				{
					// 行動
					Action( );
					
					Attack ( );
					
					// 自機弾との当たり判定
					bossToPlayerBullet();
				}
				
				break;
			case STATE.DEAD:
				{
					// 死亡
					Dead( );
					
					// 弾幕消滅
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
					
					// ステージLv.上昇
					CStageLv.GetInstance().StageLv++;
					
					// ステージレベル表示
					if( CStageLv.GetInstance().StageLv != 11 )
					{
						// ステージクリア
						new CStageClearLogo( ).Initialize( );
//						CLogoManager.GetInstance( ).Create( LOGO_ID.STAGE_CLEAR );
					}
					else if( CStageLv.GetInstance( ).StageLv == 11 )
					{
						// ゲームクリアロゴ
						new CClearLogo( ).Initialize( );
//						CLogoManager.GetInstance( ).Create( LOGO_ID.GAME_CLEAR );
					}
				}
				break;
			}
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose ()
		{
			// スプライト解放
			m_Sprite.Dispose();
		}
		
		/// <summary>
		/// 登場
		/// </summary>
		public virtual void InStage()
		{
			// 現在の位置を設定
			m_Pos = m_Sprite.Position;
			
			// ゆっくり左へ移動
			m_Pos.X -= m_Speed;
			
			// 画面内に入ったら
			if( m_Pos.X <= 700.0f )
			{
				m_Pos.X = 700.0f;
				
				// 行動状態に切り替える
				m_State = STATE.ACTION;
			}
			
			// 位置を再設定
			m_Sprite.Position = m_Pos;
		}
		
		
		/// <summary>
		/// 移動
		/// </summary>
		public virtual void Action()
		{
			switch (　m_MovePattern　) 
			{
			case MOVE_PATTERN.MOVE_FRONT:		MoveFront();	break;	// 前方(左)へ移動
			case MOVE_PATTERN.MOVE_UPDOWN:		MoveUpDown();	break;	// 上下移動
			}
		}
		
		public virtual void Attack()
		{
		}
		
		/// <summary>
		/// 死亡
		/// </summary>
		public void Dead()
		{
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
			
			// ヒットSE
			CSoundManager.GetInstance().Play( SOUND_ID.DAMEGE, 0.1f );
			
			if( m_Life <= 0 )
			{
				// 死亡エフェクト
				CreateEffect( EFFECT_ID.DEAD_EFFECT, COLOR_ID.RAINBOW, m_Pos + m_Sprite.Center );
				
				// 死亡SE
				CSoundManager.GetInstance().Play( SOUND_ID.BOMB, 0.7f );
					
				m_State = STATE.DEAD;
			}
		}
		
		/// <summary>
		/// 前移動
		/// </summary>
		public virtual void MoveFront()
		{
		}
		
		/// <summary>
		/// 上下移動
		/// </summary>
		public virtual void MoveUpDown()
		{
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
		/// 弾幕生成
		/// </summary>
		/// <param name='danmaku_id'>
		/// 弾幕ID
		/// </param>
		/// <param name='diff'>
		/// 難易度
		/// </param>
		public IDanmaku CreateDanmaku( IDanmaku danmaku,DANMAKU_ID danmaku_id, DIFFICULTY diff, COLOR_ID color)
		{
			danmaku = null;
			
			switch (danmaku_id)
			{
			case DANMAKU_ID.SAMPLE_RED:		danmaku = new CDanmakuSample( diff,color );		break;
			case DANMAKU_ID.SAMPLE2_BLUE:	danmaku = new CDanmakuSample2( diff,color );	break;
			case DANMAKU_ID.SAMPLE3:	danmaku = new CDs3( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE4:	danmaku = new CDs4( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE5:	danmaku = new CDs5( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE6:	danmaku = new CDs6( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE7:	danmaku = new CDs7( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE8:	danmaku = new CDs8( diff ,color ) ; 				break;
			case DANMAKU_ID.SAMPLE9:	danmaku = new CDs9( diff ,color ) ; 				break;
			}
			
			m_Color = color;
			
			// 初期化処理
			danmaku.Initialize( );
			
			return danmaku;
		}
		
		public  DANMAKU_ID DanmakuShuffle(DANMAKU_ID id)
		{
			int ran = aqua.CRandom.GetInstance().GetValue(1,9);
			
			switch(ran)
			{
			case 1:id = DANMAKU_ID.SAMPLE_RED;break;
			case 2:id = DANMAKU_ID.SAMPLE2_BLUE ; break;
			case 3:id = DANMAKU_ID.SAMPLE3 ; break;
			case 4:id = DANMAKU_ID.SAMPLE4 ; break;
			case 5:id = DANMAKU_ID.SAMPLE5 ; break;
			case 6:id = DANMAKU_ID.SAMPLE6 ; break;
			case 7:id = DANMAKU_ID.SAMPLE7 ; break;
			case 8:id = DANMAKU_ID.SAMPLE8 ; break;
			case 9:id = DANMAKU_ID.SAMPLE9 ; break;
			}
			
			return id;
		}
		
		public COLOR_ID ColorShuffle( COLOR_ID color)
		{
			int ran = aqua.CRandom.GetInstance().GetValue(1,10);
			
			switch(ran)
			{
			case 1:color = COLOR_ID.BLUE; break;
			case 2:color = COLOR_ID.RED; break;
			case 3:color = COLOR_ID.BLUE; break;
			case 4:color = COLOR_ID.RED; break;
			case 5:color = COLOR_ID.BLUE; break;
			case 6:color = COLOR_ID.RED; break;
			case 7:color = COLOR_ID.BLUE; break;
			case 8:color = COLOR_ID.RED; break;
			case 9:color = COLOR_ID.BLUE; break;
			case 10:color = COLOR_ID.RED; break;
			}
			
			return color;
		}
		
		/// <summary>
		/// ボスと自機弾との当たり判定
		/// </summary>
		public void bossToPlayerBullet()
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			for (int i = 0; i < list.Count; ++i)
			{
				// 自機機弾と比較
				if( list[i].ObjectID == OBJECT_ID.PLAYER_BULLET )
				{
					IPlayerBullet pbullet = (IPlayerBullet)list[i];
					
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
					if( len < 45.0f )
					{
						BeShot( pbullet.Power );
					
						// 弾解放
						pbullet.Active = false;
						
						break;
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
		/// 位置設定
		/// </summary>
		/// <value>
		/// 位置
		/// </value>
		public Vector2 Pos
		{
			get { return m_Pos; }
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
		/// 体力取得
		/// </summary>
		/// <value>
		/// 体力
		/// </value>
		public int Life
		{
			get { return m_Life; }
		}
		
		/// <summary>
		/// 状態ID
		/// </summary>
		protected enum STATE
		{
			INSTAGE,	// 登場
			ACTION,		// 行動
			DEAD,		// 死亡
		}
		
		/// <summary>
		/// 移動パターンID
		/// </summary>
		protected enum MOVE_PATTERN
		{
			MOVE_FRONT,		// 前移動
			MOVE_UPDOWN,	// 上下移動
		}
		
		/// <summary>
		/// 移動パターンID
		/// </summary>
		protected MOVE_PATTERN			m_MovePattern;
		
		/// <summary>
		/// ボス数
		/// </summary>
		private const uint				m_boss_num = 10;
		
		/// <summary>
		/// ボスの状態
		/// </summary>
		protected STATE					m_State;
		
		/// <summary>
		/// 移動パターン数
		/// </summary>
		protected const uint			m_move_num = 1;
		
		/// <summary>
		/// ボス移動間隔設定
		/// </summary>
		protected aqua.CFrameTimer		m_MoveTimer;
		
        /// <summary>
        /// スプライトクラス
        /// </summary>
        protected aqua.CSprite  		m_Sprite;
        
        /// <summary>
        /// ボスのサイズ
        /// </summary>
        protected aqua.CSize    		m_Size;
		
        /// <summary>
        /// 速度
        /// </summary>
        protected float         		m_Speed;
		
		/// <summary>
        /// 体力
        /// </summary>
        protected int           		m_Life;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2				m_Pos;
		
		/// <summary>
		/// 最大体力
		/// </summary>
		protected int 					m_max_life;
		
		/// <summary>
		/// 攻撃フラグ
		/// </summary>
		protected bool					m_CreateFlag;
		
		protected COLOR_ID				m_Color;
		
		protected IDanmaku[]				m_Danmaku;
		
		protected DANMAKU_ID[] 				m_DanmakuId;
		
		protected COLOR_ID[]				m_BulletColor;
		
	
	}
}
