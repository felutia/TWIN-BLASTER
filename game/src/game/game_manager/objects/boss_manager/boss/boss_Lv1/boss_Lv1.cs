/*!
 *  @file       boss_Lv1.cs
 *  @brief      ボスLv.1
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
	public class CBossLv1
		: IBoss
	{
		public CBossLv1 ( Vector2 pos, float speed,  int life )
			:base( pos, speed,  life)
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			// スプライト設定
			// 赤色に設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.BOSS1);
			m_Sprite.Color = new Vector4( 1.0f, 0.4f, 0.4f, 1.0f );
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 128.0f, 128.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 64.0f, 64.0f );
			
			// 行動状態
			m_MovePattern = IBoss.MOVE_PATTERN.MOVE_UPDOWN;
			
			// 状態設定
			m_State = IBoss.STATE.INSTAGE;
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 移動タイマー
			m_MoveTimer = new aqua.CFrameTimer( 120 );
			
			// 生成フラグ
			m_CreateFlag = false;
			m_Danmaku = new IDanmaku[2];
			m_DanmakuId = new DANMAKU_ID[2];
			m_BulletColor = new COLOR_ID[2];
			
			m_BulletColor[0] = ColorShuffle(m_BulletColor[0]);
			
			if(m_BulletColor[0] == COLOR_ID.BLUE)
				m_BulletColor[1] =COLOR_ID.RED;
			
			else if (m_BulletColor[0]== COLOR_ID.RED)
				m_BulletColor[1] =COLOR_ID.BLUE;
			
			for(int i = 0 ;i < 2;++i)
			{
				m_DanmakuId[i] = DanmakuShuffle(m_DanmakuId[i]);

				m_Danmaku[i] = CreateDanmaku(m_Danmaku[i] , m_DanmakuId[i],DIFFICULTY.EASY,m_BulletColor[i]);
				//m_Danmaku[i] = CreateDanmaku(m_Danmaku[i] , DANMAKU_ID.SAMPLE3,DIFFICULTY.EASY,m_BulletColor[i]);
			}
		}
		
		/// <summary>
		///  更新 
		/// </summary>
		public override void Update()
		{
			base.Update();
		}
		
		public override void Attack()
		{
			if( m_Life > m_max_life /2 )
			{
				m_Danmaku[0].Update();
				if( m_BulletColor[0] == COLOR_ID.BLUE )
					m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 1.0f );
				else if (  m_BulletColor[0] == COLOR_ID.RED )
					m_Sprite.Color = new Vector4( 1.0f, 0.4f, 0.4f, 1.0f );
			}
			else if( m_Life <= m_max_life/2 )
			{
				m_Danmaku[0].Active = false;
				
				m_Danmaku[1].Update();
				
				if( m_BulletColor[1] == COLOR_ID.BLUE )
					m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 1.0f );
				else if (  m_BulletColor[1] == COLOR_ID.RED )
					m_Sprite.Color = new Vector4( 1.0f, 0.4f, 0.4f, 1.0f );
			}
			
			if( m_Life <= 0 )
			{
				for(int i = 0 ;i < 2;++i )
				{
					m_Danmaku[i].Dispose( );
				}
			}
		}
		
		/// <summary>
		/// 上下移動
		/// </summary>
		public override void MoveUpDown()
		{
			m_Pos = m_Sprite.Position;
			
			m_Pos.Y -= m_Speed / 3.0f;
			
			if( m_Pos.Y <= 150.0f || m_Pos.Y >= 450.0f )
				m_Speed *= -1;
			
			m_Sprite.Position = m_Pos;
		}
	}
}