
/*!
 *  @file       VernierParticle.cs
 *  @brief      バーニアパーティクル
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace game
{
    /// <summary>
    /// バーニアパーティクルオブジェクトクラス
    /// </summary>
    public class CVernierParticle
        : IEffect
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CVernierParticle( COLOR_ID color, Vector2 pos )
			: base( color, pos )
        {
        }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			m_EffectID = EFFECT_ID.VERNIER_EFFECT;
			
			// スプライト生成
            m_Sprite = CSpriteManager.Create( TEXTURE_ID.V_EFFECT );
            
            // 位置設定
            m_Sprite.Position = m_Pos + new Vector2( -8.0f, 8.0f );
            
            // アルファブレンドを半加算に設定する
            m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
            
            // 拡大率設定
            m_Sprite.Scaling = Vector2.Zero;
            
            // 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 1.0f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.3f, 0.3f, 1.0f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
            
			// サイズ設定
			m_Size = new aqua.CSize( 16.0f, 16.0f );
			
            // 向き設定
            m_Direction = aqua.CRandom.GetInstance( ).GetValue( 150, 200 );
            
            // 速さ設定
            m_Speed = (float)aqua.CRandom.GetInstance( ).GetValue( 1, 30 ) / 10.0f;
		}
        
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
            Vector2 v = new Vector2( FMath.Cos( FMath.Radians( m_Direction ) ), FMath.Sin( FMath.Radians( m_Direction ) ) ) * m_Speed;
            
            m_Sprite.Position += v;
            
            m_Sprite.Color.A -= 0.025f;
            
            m_Sprite.Scaling += new Vector2( 0.05f, 0.05f );
            
            if( m_Sprite.Color.A < 0.0f )
                m_IsActive = false; 
        }
    }
}
