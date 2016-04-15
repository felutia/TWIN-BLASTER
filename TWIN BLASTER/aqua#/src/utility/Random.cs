
/*!
 *  @file       Random.cs
 *  @brief      乱数
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/06/05
 *  @since      0.1.0
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;

namespace aqua
{
    /// <summary>
    /// 乱数クラス
    /// </summary>
    public class CRandom
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CRandom GetInstance( )
        {
            // グラフィックスコンテキスト生成
            if( m_Instance == null )
            {
                m_Instance = new CRandom( );
            }
            
            return m_Instance;
        }
        
        /// <summary>
        /// 乱数取得
        /// </summary>
        /// <returns>
        /// 乱数値
        /// </returns>
        public int GetValue( )
        {
            return m_Random.Next( );
        }
        
        /// <summary>
        /// 乱数取得
        /// </summary>
        /// <returns>
        /// 乱数値
        /// </returns>
        /// <param name='max'>
        /// 乱数値の最大値
        /// </param>
        public int GetValue( int max )
        {
            return m_Random.Next( max );
        }
        
        /// <summary>
        /// 乱数取得
        /// </summary>
        /// <returns>
        /// 乱数値
        /// </returns>
        /// <param name='min'>
        /// 乱数値の最小値
        /// </param>
        /// <param name='max'>
        /// 乱数値の最大値
        /// </param>
        public int GetValue( int min, int max )
        {
            return m_Random.Next( min, max );
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CRandom( )
        {
            // 乱数クラス生成と初期化
            m_Random = new Random( Environment.TickCount );
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CRandom      m_Instance;
        
        /// <summary>
        /// 乱数クラス
        /// </summary>
        private Random              m_Random;
    }
}

