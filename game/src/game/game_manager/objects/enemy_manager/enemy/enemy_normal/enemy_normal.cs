/*!
 *  @file       enemy_normal.cs
 *  @brief      エネミーノーマル
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CEnemyNormal:IEnemy
	{
		public CEnemyNormal ( Vector2 pos, float speed, int power, int life, INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id)
			:base( pos, speed, power, life, instage_id, outstage_id )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			// スプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.ENEMY1);
			
			// プライオリティー設定
			//m_Sprite.Priority = CTextureManager.GetInstance().GetPriority(TEXTURE_ID.ENEMY);
			
			// サイズ設定
			m_Size = new aqua.CSize( 32.0f, 32.0f );
			
			// 逆様
			m_Sprite.Angle = FMath.Radians( 0.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 16.0f, 16.0f );
			
			/*
			// 攻撃状態
			m_AttackPattern = IEnemy.ATTACK_PATTERN.ATTACK_NORMAL;
			
			// 行動状態(ランダム設定)
			//Random rand = new Random();
			//int pettern = rand.Next( (int)MOVE_PATTERN.MOVE_PETTERN_NUM );
			m_MovePattern = IEnemy.MOVE_PATTERN.MOVE_FRONT;
			
			// 状態設定
			m_State = IEnemy.STATE.INSTAGE;
			*/
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 攻撃間隔設定
			m_attack_time = 30;
			
			// 攻撃タイマー設定(仮)
			m_AttackTimer = new aqua.CFrameTimer( m_attack_time );
			
			// 攻撃回数
			m_attack_num = 0;
			
		}
		
		public override void InStage_Front()
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
			
            // 画面内に入ったら
			/*
            if( m_Pos.X <= 700.0f )
            {
                m_Pos.X = 700.0f;
                
                // 攻撃状態に切り替える
                m_State = STATE.ATTACK;
            }
            */
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public override void InStage_Back()
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
			
            // 画面内に入ったら
			/*
            if( m_Pos.X <= 700.0f )
            {
                m_Pos.X = 700.0f;
                
                // 攻撃状態に切り替える
                m_State = STATE.ATTACK;
            }
            */
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public override void InStage_Up()
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
		
		public override void InStage_Down()
		{
            // 現在の位置を設定
            m_Pos = m_Sprite.Position;
            
            // ゆっくり左へ移動
            m_Pos.Y += 1;
            
			// 移動量加算
			Movement += (int)Sce.PlayStation.Core.FMath.Abs(m_Speed);
			
			// 移動しおわったら
			if( Movement > 100 )
			{
				m_State = STATE.ATTACK;
			}
			
            // 画面内に入ったら
			/*
            if( m_Pos.X <= 700.0f )
            {
                m_Pos.X = 700.0f;
                
                // 攻撃状態に切り替える
                m_State = STATE.ATTACK;
            }
            */
			
            // 位置を再設定
            m_Sprite.Position = m_Pos;
		}
		
		public override void Move_Front( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり左へ移動
	        m_Pos.X -= m_Speed;
	        
			/*
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        */
			
            // 死亡チェック
			// 移動状態だったら
			if( m_Pos.X < 0.0f   || m_Pos.X > 960.0f - 32.0f ||
				m_Pos.Y < 128.0f || m_Pos.Y > 544.0f - 32.0f )
			{
				m_State = STATE.DEAD;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void Move_Back( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり右へ移動
	        m_Pos.X += m_Speed;
	        
			/*
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        */
			
            // 死亡チェック
			// 移動状態だったら
			if( m_Pos.X < 0.0f   || m_Pos.X > 960.0f - 32.0f ||
				m_Pos.Y < 128.0f || m_Pos.Y > 544.0f - 32.0f )
			{
				m_State = STATE.DEAD;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void Move_Up( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり上へ移動
	        m_Pos.Y -= m_Speed;
	        
			/*
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        */
			
            // 死亡チェック
			// 画面外チェック
			if( m_Pos.X < 0.0f   || m_Pos.X > 960.0f - 32.0f ||
				m_Pos.Y < 128.0f || m_Pos.Y > 544.0f - 32.0f )
			{
				m_State = STATE.DEAD;
			} 
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void Move_Down( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり下へ移動
	        m_Pos.Y += m_Speed;
	        
            // 現在の位置を取得
            m_Pos = m_Sprite.Position;      
			
			/*
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        */
	        
            // 死亡チェック
			// 画面外チェック
			if( m_Pos.X < 0.0f   || m_Pos.X > 960.0f - 32.0f ||
				m_Pos.Y < 128.0f || m_Pos.Y > 544.0f - 32.0f )
			{
				m_State = STATE.DEAD;
			} 
			
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		/*
		public override void MovePattern1( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり左へ移動
	        m_Pos.X -= m_Speed;
			m_Pos.Y += 2.0f;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void MovePattern2( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
	        // ゆっくり左へ移動
	        m_Pos.X -= m_Speed;
			m_Pos.Y -= 2.0f;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void MovePattern3( )
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
			
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
			IObject player1 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
			IObject player2 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
			
			if( player1 != null )
			{
				CPlayer1 p1 = (CPlayer1)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
				
				Vector2 pos = p1.Position;
				
				float dx = pos.X - m_Pos.X;
				float dy = pos.Y - m_Pos.Y;
				float angle = FMath.Atan2( dx, dy );
				
				m_Pos.X -= 5.0f * (float)( FMath.Cos(angle));
				m_Pos.Y += 5.0f * (float)( FMath.Cos(angle));
				
				m_Pos.X -= 5;
				
				// 画面外に行ったら
				if( m_Pos.X < 0.0f )
				{
					// 死亡状態へ移行
					m_State = IEnemy.STATE.DEAD;
				}
				
				// 位置を再設定
				m_Sprite.Position = m_Pos;
			}
			
			if( player2 != null )
			{
				CPlayer2 p2 = (CPlayer2)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
				
				Vector2 pos = p2.Position;
				
				float dx = pos.X - m_Pos.X;
				float dy = pos.Y - m_Pos.Y;
				float angle = FMath.Atan2( dx, dy );
				
				m_Pos.X -= 5.0f * (float)( FMath.Cos(angle));
				m_Pos.Y += 5.0f * (float)( FMath.Cos(angle));
				
				m_Pos.X -= 5;
				
				// 画面外に行ったら
				if( m_Pos.X < 0.0f )
				{
					// 死亡状態へ移行
					m_State = IEnemy.STATE.DEAD;
				}
				
				// 位置を再設定
				m_Sprite.Position = m_Pos;
			}
		}
		
		public override void MovePattern4( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
			m_angle++;
			
			m_Pos.X -= m_Speed;
			m_Pos.Y += FMath.Sin( FMath.PI * 2.0f / 135.0f * m_angle ) * 3;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void MovePattern5( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
			
			m_angle--;
			
			m_Pos.X -= m_Speed;
			m_Pos.Y += FMath.Sin( FMath.PI * 2.0f / 135.0f * m_angle ) * 5;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void MovePattern6( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		
		public override void MovePattern7( )
		{
			// 現在の位置を設定
	        m_Pos = m_Sprite.Position;
	        
	        // 画面外に行ったら
	        if( m_Pos.X < 0.0f )
	        {
				// 死亡状態へ移行
	            m_State = IEnemy.STATE.DEAD;
	        }
	        
	        // 位置を再設定
	        m_Sprite.Position = m_Pos;
		}
		*/
		
		/// <summary>
		/// 通常攻撃
		/// </summary>
		public override void AttackNormal()
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
				
			if( m_AttackTimer.IsEnd() == true )
			{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.RAINBOW, false, new Vector2( -20.0f, -8.0f　), 0.0f, 6.0f, 1 );
				
				++m_attack_num;
				
				if( m_attack_num == 5 )
				{
					// 移動状態へ移行
					m_State = STATE.OUTSTAGE;
					
					m_attack_num = 0;
				}
			}
		}
		
		/*
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
        private void CreateBullet( ENEMY_BULLET_ID id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
        {
            CEnemyBulletManager.GetInstance( ).Create( id, OBJECT_ID.ENEMY_BULLET, color, reflec, m_Pos + m_Sprite.Center + pos, direction, speed, power );
        }
        */
	}
}