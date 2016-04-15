/*!
 *  @file       grid.cs
 *  @brief      グリッド
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
	public class CGrid:IObject
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CGrid ()
			: base()
		{
			m_ObjectID = OBJECT_ID.GRID;
		}
		
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize( )
        {
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.GRID_B_GROUND );
			m_Sprite.Position = new Vector2( 0.0f, 0.0f );
			
			m_FadeAngle = 0.0f;
        }
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
            Vector4 color = GetColor( );

			color.A = m_Sprite.Color.A;

            m_FadeAngle += 1.0f;
            
            color.A = FMath.Abs( FMath.Sin( FMath.Radians( m_FadeAngle ) ) ) * 0.75f + 0.5f;

			m_Sprite.Color = color;
		}
		
        /// <summary>
        /// 解放
        /// </summary>
        public override void Dispose( )
        {
			m_Sprite.Dispose();
        }
		
        /// <summary>
        /// 色の取得
        /// </summary>
        /// <returns>
        /// 色
        /// </returns>
        private Vector4 GetColor( )
        {
            m_Shift += 0.0001f;
            
            if( m_Shift > 0.001f )
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
        private float m_Shift = 0;
        
        /// <summary>
        /// ディフューズカラー
        /// </summary>
        private Vector4 diffuse = new Vector4( 0.5f, 0.0f, 0.0f, 1.0f );
        
		/// <summary>
        /// スプライト
        /// </summary>
        private aqua.CSprite		m_Sprite;
		
        /// <summary>
        /// フェード用アングル
        /// </summary>
        private float				m_FadeAngle;
	}
}

