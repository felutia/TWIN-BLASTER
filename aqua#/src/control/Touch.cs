
/*!
 *  @file       Touch.cs
 *  @brief      タッチ入力
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/05/30
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace aqua
{
    /// <summary>
    /// フリックの向き
    /// </summary>
    public enum FLICK_DIRECTION
    {
          NONE      // フリックなし
        , LEFT      // 左向き
        , RIGHT     // 右向き
        , UP        // 上向き
        , DOWN      // 下向き
    }
    
    /// <summary>
    /// カスタムタッチデータクラス
    /// </summary>
    internal class CTouchData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTouchData( )
        {
            id          = -1;
            absolute    = new Vector2[2];
            relative    = new Vector2[2];
            start       = Vector2.Zero;
            state       = TouchStatus.None;
        }
        
        /// <summary>
        /// 新規データの登録
        /// </summary>
        /// <param name='id'>
        /// ID
        /// </param>
        /// <param name='x'>
        /// X座標
        /// </param>
        /// <param name='y'>
        /// Y座標
        /// </param>
        public void SetNewData( int id, float x, float y )
        {
            this.id     = id;
            
            state       = TouchStatus.None;
            
            start.X     = x;
            start.Y     = y;
            
            absolute[0].X = absolute[1].X = x;
            absolute[0].Y = absolute[1].Y = y;
            
            relative[0] = relative[1] = Vector2.Zero;
        }
        
        /// <summary>
        /// 識別子
        /// </summary>
        public int          id;
        
        /// <summary>
        /// 絶対位置
        /// </summary>
        public Vector2[]    absolute;
        
        /// <summary>
        /// 相対位置
        /// </summary>
        public Vector2[]    relative;
        
        /// <summary>
        /// タッチ開始位置
        /// </summary>
        public Vector2      start;
        
        /// <summary>
        /// タッチ状態
        /// </summary>
        public TouchStatus  state;
    }
    
    /// <summary>
    /// タッチ判定クラス
    /// </summary>
    public static class CTouch
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize( )
        {
            // タッチデータ生成
            m_TouchData = new CTouchData[m_max_touch_count];
            
            for( int i = 0; i < m_max_touch_count; ++i )
                m_TouchData[i] = new CTouchData( );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public static void Update( )
        {
            try
            {
                // タッチデータ取得
                List<TouchData> touch_data = Touch.GetData( 0 );
                
                for( int i = 0; i < m_max_touch_count; ++i )
                {
                    // 指が離されている
                    if( m_TouchData[i].state == TouchStatus.Up )
                    {
                        m_TouchData[i].id = -1;
                        
                        m_TouchData[i].state = TouchStatus.None;
                        
                        --m_TouchCount;
                    }
                }
                
                int index = -1;
                
                foreach( var td in touch_data )
                {
                    // IDが一致するものを検索
                    for( int i = 0; i < m_max_touch_count; ++i )
                    {
                        if( m_TouchData[i].id == td.ID )
                        {
                            index = i;
                            
                            break;
                        }
                    }
                    
                    // 新規タッチ検出
                    if( index < 0 && ( td.Status == TouchStatus.Down || td.Status == TouchStatus.Up ) )
                    {
                        for( int i = 0; i < m_max_touch_count; ++i )
                        {
                            if( m_TouchData[i].id < 0 )
                            {
                                // 新規データ追加
                                m_TouchData[i].SetNewData( td.ID, td.X, td.Y );
                                
                                if( td.Status == TouchStatus.Up )
                                    m_TouchData[i].state = TouchStatus.Up;
                                else
                                    m_TouchData[i].state = TouchStatus.Down;
                                
                                ++m_TouchCount;
                                
                                break;
                            }
                        }
                    }
                    // 既存タッチ
                    else
                    {
                        if( td.Status != TouchStatus.Down )
                            m_TouchData[index].state = TouchStatus.Move;
                        
                        // 絶対位置算出
                        // 前の位置保存
                        m_TouchData[index].absolute[1] = m_TouchData[index].absolute[0];
                        
                        // 現在位置設定
                        m_TouchData[index].absolute[0].X = td.X;
                        m_TouchData[index].absolute[0].Y = td.Y;
                        
                        // 相対位置算出
                        // 前の位置保存
                        m_TouchData[index].relative[1] = m_TouchData[index].relative[0];
                        
                        // 現在位置算出
                        m_TouchData[index].relative[0] = m_TouchData[index].absolute[0] - m_TouchData[index].absolute[1];
                        
                        // 離された
                        if( td.Status == TouchStatus.Up || td.Status == TouchStatus.Canceled )
                            m_TouchData[index].state = TouchStatus.Up;
                    }
                }
            }
            catch( Exception )
            {
                // タッチデータの取得が行われていない
                // もしくはタッチデータの取得設定がされていない
                // デフォルト値を返す
//                m_TouchData = new List<TouchData>( );
            }
        }
        
        /// <summary>
        /// タッチ状態チェック
        /// </summary>
        /// <param name='state'>
        /// タッチ状態
        /// </param>
        public static bool TouchState( TouchStatus state )
        {
            for( int i = 0; i < m_max_touch_count; ++i )
            {
                if( m_TouchData[i].id < 0 ) continue;
                
                if( m_TouchData[i].state == state )
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// タッチのX座標取得
        /// </summary>
        /// <returns>
        /// タッチのX座標
        /// </returns>
        public static int TouchX( )
        {
            for( int i = 0; i < m_max_touch_count; ++i )
            {
                if( m_TouchData[i].id < 0 ) continue;
                
                if( m_TouchData[i].state == TouchStatus.Down || m_TouchData[i].state == TouchStatus.Move || m_TouchData[i].state == TouchStatus.Up )
                    return TouchXToScreenX( m_TouchData[i].absolute[0].X );
            }
            
            return -1;
        }
        
        /// <summary>
        /// タッチのY座標取得
        /// </summary>
        /// <returns>
        /// タッチのY座標
        /// </returns>
        public static int TouchY( )
        {
            for( int i = 0; i < m_max_touch_count; ++i )
            {
                if( m_TouchData[i].id < 0 ) continue;
                
                if( m_TouchData[i].state == TouchStatus.Down || m_TouchData[i].state == TouchStatus.Move || m_TouchData[i].state == TouchStatus.Up )
                    return TouchYToScreenY( m_TouchData[i].absolute[0].Y );
            }
            
            return -1;
        }
        
        /// <summary>
        /// フリック判定
        /// </summary>
        /// <param name='dir'>
        /// 判定する方向
        /// </param>
        public static bool Flick( FLICK_DIRECTION flick_direction )
        {
            if( m_TouchCount > 1 ) return false;
            
            for( int i = 0; i < m_max_touch_count; ++i )
            {
                if( m_TouchData[i].id < 0 ) continue;
                
                if( m_TouchData[i].state == TouchStatus.Up )
                    continue;
                
                // 相対位置取得
                Vector2 vec  = m_TouchData[i].relative[0];
                Vector2 avec = Vector2.Abs( vec );
                
                FLICK_DIRECTION dir = FLICK_DIRECTION.NONE;
                
                // 向きを算出
                if( avec.X > avec.Y )
                {
                    float x = avec.X;// * (float)(CGameFramework.ScreenWidth * 25.4f) / Sce.PlayStation.Core.Environment.SystemParameters.DisplayDpiX;
                    
                    if( x >= m_flick_sensitivity )
                    {
                        if( vec.X < 0.0f )
                            dir = FLICK_DIRECTION.LEFT;
                        else
                            dir = FLICK_DIRECTION.RIGHT;
                    }
                }
                else
                {
                    float y = avec.Y;// * (float)(CGameFramework.ScreenHeight * 25.4f) / Sce.PlayStation.Core.Environment.SystemParameters.DisplayDpiY;
                    
                    if( y >= m_flick_sensitivity )
                    {
                        if( vec.Y < 0.0f )
                            dir = FLICK_DIRECTION.UP;
                        else
                            dir = FLICK_DIRECTION.DOWN;
                    }
                }
                
                // 向きが一致していればフリック判定
                if( dir == flick_direction )
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// 矩形範囲内のタッチチェック
        /// </summary>
        /// <param name='rect'>
        /// 矩形
        /// </param>
        public static bool CheckTouchRect( CRect rect )
        {
            return CheckTouchRect( (int)rect.left, (int)rect.top, (int)rect.right, (int)rect.bottom );
        }
        
        /// <summary>
        /// 矩形範囲内のタッチチェック
        /// </summary>
        /// <param name='left'>
        /// 左端
        /// </param>
        /// <param name='top'>
        /// 上端
        /// </param>
        /// <param name='right'>
        /// 右端
        /// </param>
        /// <param name='bottom'>
        /// 下端
        /// </param>
        public static bool CheckTouchRect( int left, int top, int right, int bottom )
        {
            int x = TouchX( );
            int y = TouchY( );
            
            if( left < x && right > x && top < y && bottom > y )
                return true;
            
            return false;
        }
        
        /// <summary>
        /// 円範囲内のタッチチェック
        /// </summary>
        /// <param name='cx'>
        /// 中心座標X
        /// </param>
        /// <param name='cy'>
        /// 中心座標Y
        /// </param>
        /// <param name='radius'>
        /// 半径
        /// </param>
        public static bool CheckTouchCircle( int cx, int cy, int radius )
        {
            int x = TouchX( );
            int y = TouchY( );
            
            // 二点間の距離から判定する
            int d = ( x - cx ) * ( x - cx ) + ( y - cy ) * ( y - cy );
            
            if( d < radius * radius )
                return true;
            
            return false;
        }
        
        /// <summary>
        /// 移動量取得
        /// </summary>
        public static Vector2 Movement( )
        {
            for( int i = 0; i < m_max_touch_count; ++i )
            {
                if( m_TouchData[i].id < 0 ) continue;
                
                if( m_TouchData[i].state == TouchStatus.Down || m_TouchData[i].state == TouchStatus.Move || m_TouchData[i].state == TouchStatus.Up )
                {
                    Vector2 v1 = m_TouchData[i].absolute[0];
                    
                    v1.X = ( v1.X + 0.5f ) * CGameFramework.ScreenWidth;
                    v1.Y = ( v1.Y + 0.5f ) * CGameFramework.ScreenHeight;
                    
                    Vector2 v2 = m_TouchData[i].absolute[1];
                    
                    v2.X = ( v2.X + 0.5f ) * CGameFramework.ScreenWidth;
                    v2.Y = ( v2.Y + 0.5f ) * CGameFramework.ScreenHeight;
                    
                    Vector2 v = v1 - v2;
                    
                    return v;
                }
            }
            
            return Vector2.Zero;
        }
        
        /// <summary>
        /// タッチ座標をスクリーン座標に変換
        /// </summary>
        /// <returns>
        /// スクリーンのX座標
        /// </returns>
        /// <param name='x'>
        /// タッチされた位置のX座標
        /// </param>
        private static int TouchXToScreenX( float x )
        {
            return (int)( ( x + 0.5f ) * CGameFramework.ScreenWidth );
        }
        
        /// <summary>
        /// タッチ座標をスクリーン座標に変換
        /// </summary>
        /// <returns>
        /// スクリーンのY座標
        /// </returns>
        /// <param name='y'>
        /// タッチされた位置のY座標
        /// </param>
        private static int TouchYToScreenY( float y )
        {
            return (int)( ( y + 0.5f ) * CGameFramework.ScreenHeight );
        }
        
        /// <summary>
        /// 最大タッチ数
        /// PSVITA自体は最大6まで
        /// </summary>
        private const int               m_max_touch_count   = 4;
        
        /// <summary>
        /// フリック感度
        /// </summary>
        private const float             m_flick_sensitivity = 0.0f;
        
        /// <summary>
        /// タッチ数
        /// </summary>
        private static int              m_TouchCount        = 0;
        
        /// <summary>
        /// カスタムタッチデータ
        /// </summary>
        private static CTouchData[]     m_TouchData         = null;
    }
}
