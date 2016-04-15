/*!
 *  @file       stage_Lv.cs
 *  @brief      ステージレベルUI(右上)
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
	public class CStageLv
	{
		/// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CStageLv GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CStageLv( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CStageLv()
		{
            m_StageLv = 1;
        }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			m_StageLv = 1;
			
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.LEVEL_LOGO);
			
			m_Sprite.Position = new Vector2( 630.0f, 10.0f );
			
			m_Number = new CNumber( );
			
			m_Number.Initialize( new Vector2( 850.0f, 18.0f ), new Vector2( 1.0f, 1.0f ), 2, m_StageLv );
			
			m_ClearTimer = new aqua.CFrameTimer( 60 );
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			if( m_StageLv == 11 )
			{
				m_ClearTimer.Update( );
				
				if( m_ClearTimer.IsEnd( ) == true )
				{
					// ステージ表示10に変更
					m_StageLv = 10;
				}
			}
			else if( m_StageLv != 11 )
				m_Number.Number = m_StageLv;
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public void Dispose()
		{
			// スプライト解放
			m_Sprite.Dispose();
			
			m_Number.Active = false;
		}
		
		/// <summary>
		/// ステージレベルの取得
		/// </summary>
		/// <value>
		/// ステージレベル
		/// </value>
		public int StageLv
		{
			get { return m_StageLv;  }
			set { m_StageLv = value; }
		}
		
		/// <summary>
        /// インスタンス
        /// </summary>
        private static CStageLv		m_Instance;
		
		/// <summary>
		/// スプライト
		/// </summary>
		private aqua.CSprite		m_Sprite;
		
		/// <summary>
		/// ゲームクリア用タイマー
		/// </summary>
		private aqua.CFrameTimer	m_ClearTimer;
		
		/// <summary>
		/// 数字表示
		/// </summary>
		private CNumber				m_Number;
		
		/// <summary>
		/// ステージレベル
		/// </summary>
		private int					m_StageLv;
	}
}

