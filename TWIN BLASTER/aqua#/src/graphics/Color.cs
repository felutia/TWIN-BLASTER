
/*!
 *  @file       Color.cs
 *  @brief      色
 *  @author     Kazuya Maruyama
 *  @date       2014/01/05
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace aqua
{
    /// <summary>
    /// 色クラス
    /// </summary>
    public class CColor
    {
        /// <summary>
        /// 16進数の色をVector4に変換
        /// </summary>
        /// <returns>
        /// Vector4型の色
        /// </returns>
        /// <param name='color'>
        /// 16進数の色
        /// </param>
        public static Vector4 UintToVector4( uint color )
        {
            float r = ( ( color & 0xff000000 ) >> 24 ) / 255.0f;
            float g = ( ( color & 0x00ff0000 ) >> 16 ) / 255.0f;
            float b = ( ( color & 0x0000ff00 ) >>  8 ) / 255.0f;
            float a = ( ( color & 0x000000ff ) )       / 255.0f;
            
            return new Vector4( r, g, b, a );
        }
        
        /// <summary>
        /// Vector4の色を16進数に変換
        /// </summary>
        /// <returns>
        /// 16進数の色
        /// </returns>
        /// <param name='color'>
        /// Vector4型の色
        /// </param>
        public static uint Vector4ToUint( Vector4 color )
        {
            byte r = (byte)( color.R * 255.0f );
            byte g = (byte)( color.G * 255.0f );
            byte b = (byte)( color.B * 255.0f );
            byte a = (byte)( color.A * 255.0f );
            
            return (uint)( ( r << 24 ) | ( g << 16 ) | ( b << 8 ) | a );
        }
        
        /// <summary>
        /// 白
        /// </summary>
        public static readonly Vector4     White   = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
        
        /// <summary>
        /// 黒
        /// </summary>
        public static readonly Vector4     Black   = new Vector4( 0.0f, 0.0f, 0.0f, 1.0f );
        
        /// <summary>
        /// 赤
        /// </summary>
        public static readonly Vector4     Red     = new Vector4( 1.0f, 0.0f, 0.0f, 1.0f );
        
        /// <summary>
        /// 緑
        /// </summary>
        public static readonly Vector4     Green   = new Vector4( 0.0f, 1.0f, 0.0f, 1.0f );
        
        /// <summary>
        /// 青
        /// </summary>
        public static readonly Vector4     Blue    = new Vector4( 0.0f, 0.0f, 1.0f, 1.0f );
        
        /// <summary>
        /// シアン
        /// </summary>
        public static readonly Vector4     Cyan    = new Vector4( 0.0f, 1.0f, 1.0f, 1.0f );
        
        /// <summary>
        /// イエロー
        /// </summary>
        public static readonly Vector4     Yellow  = new Vector4( 1.0f, 1.0f, 0.0f, 1.0f );
        
        /// <summary>
        /// マゼンタ
        /// </summary>
        public static readonly Vector4     Magenta = new Vector4( 1.0f, 0.0f, 1.0f, 1.0f );
    }
}
