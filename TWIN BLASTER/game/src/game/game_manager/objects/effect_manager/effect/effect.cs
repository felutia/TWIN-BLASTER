/*!
 *  @file       effect.cs
 *  @brief      エフェクトベースクラス
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// エフェクトクラス
    /// </summary>
    public abstract class IEffect : IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IEffect( COLOR_ID color, Vector2 pos )
			: base()
        {
			// 色設定
			m_Color 		= color;
			
			// 位置設定
            m_Pos			= pos;
        }
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update()
		{
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose ()
		{
			m_Sprite.Dispose();
		}
        
        /// <summary>
        /// スプライトの取得
        /// </summary>
        /// <value>
        /// スプライト
        /// </value>
        public aqua.CSprite Sprite
        {
            get { return m_Sprite; }
        }
        
        /// <summary>
        /// サイズの取得
        /// </summary>
        /// <value>
        /// サイズ
        /// </value>
        public aqua.CSize Size
        {
            get { return m_Size; }
        }
		
		/// <summary>
        /// 色の取得
        /// </summary>
        /// <value>
        /// 色
        /// </value>
        public COLOR_ID Color
        {
            get { return m_Color; }
        }
		
		/// <summary>
        /// エフェクトIDアクセッサ
        /// </summary>
        /// <value>
        /// エフェクトID
        /// </value>
        public EFFECT_ID EffectID
        {
            get { return m_EffectID; }
        }
		
        /// <summary>
        /// 位置の取得と設定
        /// </summary>
        /// <value>
        /// 位置
        /// </value>
        public Vector2 Position
        {
            get { return m_Sprite.Position;  }
            set { m_Sprite.Position = value; }
        }
		
        /// <summary>
        /// スプライトクラス
        /// </summary>
        protected aqua.CSprite		m_Sprite;
        
        /// <summary>
        /// エフェクトのサイズ
        /// </summary>
        protected aqua.CSize    	m_Size;
	
		/// <summary>
		/// アニメーションタイマー
		/// </summary>
		protected aqua.CFrameTimer	m_AnimaTimer;
		
		/// <summary>
		/// アニメーションフレーム数
		/// </summary>
		protected int				m_AnimaFrame;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2			m_Pos;
		
        /// <summary>
        /// 向き
        /// </summary>
        protected float				m_Direction;
        
        /// <summary>
        /// 速さ
        /// </summary>
        protected float				m_Speed;
		
		/// <summary>
		/// 色設定
		/// </summary>
		protected COLOR_ID			m_Color;
		
		/// <summary>
		/// エフェクトID
		/// </summary>
		protected EFFECT_ID			m_EffectID;
    }
}
