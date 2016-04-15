
/*!
 *  @file       GamePad.cs
 *  @brief      ゲームパッド入力
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/06/28
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core;

namespace aqua
{
    /// <summary>
    /// ボタン入力判定クラス
    /// </summary>
    public static class CGamePad
    {
        /// <summary>
        /// 更新
        /// </summary>
        public static void Update( )
        {
            try
            {
                // ゲームパッドデータ取得
                m_GamePadData = GamePad.GetData( 0 );
            }
            catch( Exception )
            {
                // パッドデータの取得が行われていない
                // もしくはパッドデータの取得設定がされていない
                // デフォルト値を返す
//                m_GamePadData = new GamePadData( );
            }
        }
        
        /// <summary>
        /// ボタン入力判定
        /// </summary>
        /// <param name='button'>
        /// ボタンID
        /// </param>
        public static bool Button( GamePadButtons button )
        {
            if( ( m_GamePadData.Buttons & ( button ) ) != 0 )
                return true;
            
            return false;
        }
        
        /// <summary>
        /// トリガー入力判定
        /// </summary>
        /// <param name='button'>
        /// ボタンID
        /// </param>
        public static bool Trigger( GamePadButtons button )
        {
            if( ( m_GamePadData.ButtonsDown & ( button ) ) != 0 )
                return true;
            
            return false;
        }
        
        /// <summary>
        /// リリースド入力判定
        /// </summary>
        /// <param name='button'>
        /// ボタンID
        /// </param>
        public static bool Released( GamePadButtons button )
        {
            if( ( m_GamePadData.ButtonsUp & ( button ) ) != 0 )
                return true;
            
            return false;
        }
        
        /// <summary>
        /// 左アナログスティック情報
        /// </summary>
        /// <returns>
        /// スティックの状態(-1.0f～1.0f)
        /// </returns>
        public static Vector2 LeftAnalogStick( )
        {
            Vector2 v = Vector2.Zero;
            
            v.X =  m_GamePadData.AnalogLeftX;
            v.Y =  m_GamePadData.AnalogLeftY;
            
            return v;
        }
        
        /// <summary>
        /// 右アナログスティック情報
        /// </summary>
        /// <returns>
        /// スティックの状態(-1.0f～1.0f)
        /// </returns>
        public static Vector2 RightAnalogStick( )
        {
            Vector2 v = Vector2.Zero;
            
            v.X =  m_GamePadData.AnalogRightX;
            v.Y =  m_GamePadData.AnalogRightY;
            
            return v;
        }
        
        /// <summary>
        /// ゲームパッドデータ
        /// </summary>
        private static GamePadData m_GamePadData;
    }
}
