/*!
 *  @file       stage.cs
 *  @brief      ステージクラス
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
	public class CStage:IObject
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CStage ()
			: base()
		{
			m_ObjectID = OBJECT_ID.STAGE;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			m_Sprite = new aqua.CSprite[m_sprite_num];
			
			// 背景
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_Sprite[i] = CSpriteManager.Create( TEXTURE_ID.B_GROUND );
				m_Sprite[i].Position = new Vector2( 1920.0f * (float)i, 0.0f );
			}
			
			int random = aqua.CRandom.GetInstance().GetValue(0,9);
			
			switch ( random )
			{
				case 0 : aqua.CBgm.Load( "data/sound/Stage_01.mp3" );break;
				case 1 : aqua.CBgm.Load( "data/sound/Stage_02.mp3" );break;
				case 2 : aqua.CBgm.Load( "data/sound/Stage_03.mp3" );break;
				case 3 : aqua.CBgm.Load( "data/sound/Stage_01.mp3" );break;
				case 4 : aqua.CBgm.Load( "data/sound/Stage_02.mp3" );break;
				case 5 : aqua.CBgm.Load( "data/sound/Stage_03.mp3" );break;
				case 6 : aqua.CBgm.Load( "data/sound/Stage_01.mp3" );break;
				case 7 : aqua.CBgm.Load( "data/sound/Stage_02.mp3" );break;
				case 8 : aqua.CBgm.Load( "data/sound/Stage_03.mp3" );break;
				case 9 : aqua.CBgm.Load( "data/sound/Stage_01.mp3" );break;
			}
			
			aqua.CBgm.Play( );
			aqua.CBgm.Volume = 0.5f;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			// スクロール
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_Sprite[i].Position.X -= 1.0f;
				
				if( m_Sprite[i].Position.X < -1920.0f )
					m_Sprite[i].Position.X += 1920.0f * 2.0f;
			}
		}

		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			for( int i = 0; i < m_sprite_num; ++i )
				m_Sprite[i].Dispose( );
			
			
			aqua.CBgm.Unload( );
        }
		
        /// <summary>
        /// スプライト数
        /// </summary>
        private const int           m_sprite_num = 2;
		
		/// <summary>
		/// 背景スプライト
		/// </summary>
		private aqua.CSprite[]		m_Sprite;
	}
}

