/*!
 *  @file       unit.cs
 *  @brief      ユニットクラス
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;

namespace game
{
	public class CUnit
	{
		/// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CUnit GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CUnit( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CUnit()
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			// 最大値設定
			m_max_life	= 99;
			m_max_bom	= 99;
			
			// 初期値設定
			m_Life		= 3;
			m_BomNum	= 3;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			if( m_Life > m_max_life )
				m_Life = m_max_life;
			
			if( m_Life < 0 )
				m_Life = 0;
			
			if( m_BomNum > m_max_bom )
				m_BomNum = m_max_bom;
			
			if( m_BomNum < 0 )
				m_BomNum = 0;
		}
		
		/// <summary>
		/// 残機取得
		/// </summary>
		/// <value>
		/// 残機
		/// </value>
		public int Life
		{
			get{ return m_Life; }
			set{ m_Life = value; }
		}
		
		/// <summary>
		/// ボム取得
		/// </summary>
		/// <value>
		/// ボム
		/// </value>
		public int Bom
		{
			get{ return m_BomNum; }
			set{ m_BomNum = value; }
		}
		
		/// <summary>
		/// 数値種別
		/// </summary>
		public enum TYPE
		{
			LIFE,	// 残機
			BOM,	// ボム
		}
		
		/// <summary>
        /// インスタンス
        /// </summary>
        private static CUnit	m_Instance;
		
		/// <summary>
		/// 最大残機数
		/// </summary>
		private int				m_max_life;
		
		/// <summary>
		/// 自機残機共通
		/// </summary>
		private int				m_Life;
		
		/// <summary>
		/// 最大ボム数
		/// </summary>
		private int				m_max_bom;
		
		/// <summary>
		/// ボム数(自機2機で共有)
		/// </summary>
		private int				m_BomNum;
	}
}

