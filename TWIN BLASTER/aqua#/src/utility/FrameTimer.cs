
/*!
 *  @file       FrameTimer.cs
 *  @brief      フレームタイマー
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/05/09
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

namespace aqua
{
    /// <summary>
    /// フレームタイマークラス
    /// </summary>
    public class CFrameTimer
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CFrameTimer( )
        {
            m_Frame = 0;
            m_Limit = 0;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='limit'>
        /// タイマーの限界フレーム
        /// </param>
        public CFrameTimer( uint limit )
        {
            m_Frame = 0;
            m_Limit = limit;
        }
        
        /// <summary>
        /// タイマー更新
        /// </summary>
        public void Update( )
        {
            ++m_Frame;
        }
        
        /// <summary>
        /// タイマー終了判定
        /// </summary>
        /// <returns>
        /// <c>true</c> タイマーが終了フレームになった <c>false</c>.
        /// </returns>
        public bool IsEnd( )
        {
            if( m_Frame > m_Limit )
            {
                m_Frame = 0;
                
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// 限界時間の取得と設定
        /// </summary>
        /// <value>
        /// タイマーの限界フレーム
        /// </value>
        public uint Limit
        {
            get { return m_Limit;  }
            set { m_Limit = value; }
        }
        
        /// <summary>
        /// 現在のフレーム数取得
        /// </summary>
        /// <value>
        /// フレーム数
        /// </value>
        public uint Frame
        {
            get { return m_Frame;  }
            set { m_Frame = value; }
        }
        
        /// <summary>
        /// 現在のフレーム数
        /// </summary>
        private uint m_Frame;
        
        /// <summary>
        /// 限界フレーム数
        /// </summary>
        private uint m_Limit;
    }
}
