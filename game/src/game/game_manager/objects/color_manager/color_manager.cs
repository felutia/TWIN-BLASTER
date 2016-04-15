
/*!
 *  @file       color_manager.cs
 *  @brief      カラー管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// カラー管理クラス
    /// </summary>
    public static class CColorManager
    {
        /// <summary>
        /// 色の取得
        /// </summary>
        /// <returns>
        /// 色
        /// </returns>
        public static Vector4 GetColor( )
        {
            m_Shift += 0.001f;
            
            if( m_Shift > 0.01f )
                m_Shift = 0.0f;
            
            Vector4 color   = Vector4.One;
            Vector4 hsv     = Vector4.One;
            
            // RGB To HSV
            float max_ = FMath.Max( diffuse.R, FMath.Max( diffuse.G, diffuse.B ) );
            float min_ = FMath.Min( diffuse.R, FMath.Min( diffuse.G, diffuse.B ) );
            
            float delta = max_ - min_;
            
            hsv.B = max_;
            
            //if( max_ != 0.0f )
            //    hsv.G = delta / max_;
            //else
            //    hsv.G = 0.0f;
            
            if( diffuse.R == max_ )
                hsv.R = ( diffuse.G - diffuse.B ) / delta;
            else if( diffuse.G == max_ )
                hsv.R = 2.0f + ( diffuse.B - diffuse.R ) / delta;
            else
                hsv.R = 4.0f + ( diffuse.R - diffuse.G ) / delta;
            
            hsv.R /= 6.0f;
            
            if( hsv.R < 0.0f ) hsv.R += 1.0f;
            
            // 色相補正
            hsv.R += m_Shift;
            
//            if( 1.0f <= hsv.R ) hsv.R -= 1.0f;
            
            // HSV To RGB
            if( hsv.G == 0.0f )
            {
                color.R = color.G = color.B = hsv.B;
            }
            else
            {
                if( 1.0f <= hsv.R ) hsv.R -= 1.0f;
                
                hsv.R *= 6.0f;
                float i = FMath.Floor( hsv.R );
                float f = hsv.R - i;
                float aa = hsv.B * ( 1.0f - hsv.G );
                float bb = hsv.B * ( 1.0f - ( hsv.G * f ) );
                float cc = hsv.B * ( 1.0f - hsv.G * ( 1.0f - f ) );
                
                if( i < 1.0f )
                {
                    color.R = hsv.B;
                    color.G = cc;
                    color.B = aa;
                }
                else if( i < 2 )
                {
                    color.R = bb;
                    color.G = hsv.B;
                    color.B = aa;
                }
                else if( i < 3 )
                {
                    color.R = aa;
                    color.G = hsv.B;
                    color.B = cc;
                }
                else if( i < 4 )
                {
                    color.R = aa;
                    color.G = bb;
                    color.B = hsv.B;
                }
                else if( i < 5 )
                {
                    color.R = cc;
                    color.G = aa;
                    color.B = hsv.B;
                }
                else
                {
                    color.R = hsv.B;
                    color.G = aa;
                    color.B = bb;
                }
            }
            
            diffuse = color;
            
            return color;
        }
        
        /// <summary>
        /// 色相環のシフト値
        /// </summary>
        private static float m_Shift = 0;
        
        /// <summary>
        /// ディフューズカラー
        /// </summary>
        private static Vector4 diffuse = new Vector4( 1.0f, 0.0f, 0.0f, 1.0f );
    }
}
