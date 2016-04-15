/*!
 *  @file       number.cs
 *  @brief      数字UI
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
	public class CNumber
		: IObject
	{
		public CNumber ()
			:base()
		{
			m_Sprite = null;
			m_Digit = 1;
			m_Number = 0;
			m_NextNumber = 0;
			m_DigitWidth = 0;
			m_DigitHeight = 0;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		/// 位置
		/// Position.
		/// </param>
		/// 最大桁数
		/// Max_digit.
		/// </param>
		/// 数値
		/// Number.
		/// </param>
		public void Initialize( Vector2 position, Vector2 scale, int max_digit, int number )
		{
			m_Digit = max_digit;
			
			m_Sprite = new aqua.CSprite[max_digit];
			
			m_NextNumber = m_Number = number;
			
			m_Position = position;
			
			m_Scale = scale;
			
			aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, m_DigitWidth, m_DigitHeight );
			
			for (int i = 0; i < m_Digit; i++)
			{
				m_Sprite[i] = CSpriteManager.Create(TEXTURE_ID.NUMBER);
				
				m_Sprite[i].Rect = rect;
				
				m_Sprite[i].Scaling = m_Scale;
				
				m_DigitWidth = m_Sprite[i].TextureWidth / 10;
				m_DigitHeight = m_Sprite[i].TextureHeight;
			}
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update()
		{
			if( m_NextNumber > m_Number )
			{
				++m_Number;
				
				int max = (int)Math.Pow( (float)10, m_Digit ) - 1;
				
				if( m_Number > max )
					m_Number = max;
			}
			else if( m_NextNumber < m_Number )
			{
				--m_Number;
				
				if( m_Number < 0 ) m_Number = 0;
			}
		
			// 読み込み範囲算出
			CalcNumberRect( );
		
			for( int i = 0; i < m_Digit; ++i )	
				m_Sprite[i].Update( );
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			// テクスチャ解放
			for (int i = 0; i < m_Digit; i++)
			{
				m_Sprite[i].Dispose( );
			}
		}
		
		/// <summary>
		/// 数値取得設定
		/// </summary>
		/// <value>
		/// 数値
		/// </value>
		public int Number
		{
		    get{ return m_NextNumber;  }
			set{ m_NextNumber = value; }
		}
		
		/// <summary>
		/// 数値加算
		/// </summary>
		/// <param name='number'>
		/// 数値
		/// </param>
		public void AddNumber( int number )
		{
		    m_NextNumber = number;
		}
		
		/// <summary>
        /// 位置の取得と設定
        /// </summary>
        /// <value>
        /// 位置
        /// </value>
		public Vector2 Position
		{
			get { return m_Position;  }
			set { m_Position = value; }
		}
		
		/// <summary>
		/// テクスチャ読み込み範囲設定
		/// </summary>
		public void CalcNumberRect( )
		{
		    int number = m_Number;
		
		    int digit_count = 0;
		
		    do
		    {
		        int d = number % 10;
		
		        aqua.CRect rect = new aqua.CRect( d * m_DigitWidth, 0, d * m_DigitWidth + m_DigitWidth, m_DigitHeight );
		
		        m_Sprite[digit_count].Rect = rect;
		
		        Vector2 pos = m_Position + new Vector2((float)( m_DigitWidth * ( m_Digit - ( digit_count + 1 ) ) ), 0.0f) * m_Scale;
		
		        m_Sprite[digit_count].Position = pos;
		
		        number /= 10;
		
		        ++digit_count;
		    }
		    while( digit_count < m_Digit );
		}
		
		/// <summary>
		/// スプライト
		/// </summary>
		private aqua.CSprite[]	m_Sprite;
		
		/// <summary>
		/// 桁数
		/// </summary>
		private int				m_Digit;
		
		/// <summary>
		/// 位置
		/// </summary>
		private Vector2			m_Position;
		
		/// <summary>
		/// 拡大値
		/// </summary>
		private Vector2			m_Scale;
		
		/// <summary>
		/// 数値
		/// </summary>
		private int				m_Number;
		
		/// <summary>
		/// 次の数値
		/// </summary>
		private int				m_NextNumber;
		
		/// <summary>
		/// 一桁あたりの横幅
		/// </summary>
		private float			m_DigitWidth;
		
		/// <summary>
		/// 一桁あたりの縦幅
		/// </summary>
		private float			m_DigitHeight;
	}
}

