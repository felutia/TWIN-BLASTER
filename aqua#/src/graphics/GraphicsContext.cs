
/*!
 *  @file       GraphicsContext.cs
 *  @brief      描画管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
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
    /// 描画管理クラス
    /// </summary>
    public class CGraphicsContext
        : IDisposable
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CGraphicsContext GetInstance( )
        {
            // グラフィックスコンテキスト生成
            if( m_Instance == null )
                m_Instance = new CGraphicsContext( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize( )
        {
            // グラフィックスコンテキスト生成
            if( m_GraphicsContext == null )
                m_GraphicsContext = new GraphicsContext( );
            
            // クリアカラー
            m_ClearColor = m_default_clear_color;
            
            // グラフィックスコンテキストにクリアカラー設定
            m_GraphicsContext.SetClearColor( m_ClearColor );
            
            // 白のテクスチャ読み込み
            if( m_WhiteTexture == null )
                m_WhiteTexture = core.CTextureManager.GetInstance( ).Load( "aqua.resources.white.png", true );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
            // 白のテクスチャ解放
            core.CTextureManager.GetInstance( ).Unload( "aqua.resources.white.png" );
            
            // グラフィックスコンテキスト解放
            m_GraphicsContext.Dispose( );
        }
        
        /// <summary>
        /// 画面クリア
        /// </summary>
        public void Clear( )
        {
            m_GraphicsContext.Clear( );
        }
        
        /// <summary>
        /// 画面転送
        /// </summary>
        public void SwapBuffer( )
        {
            m_GraphicsContext.SwapBuffers( );
        }
        
        /// <summary>
        /// アルファブレンドの設定
        /// </summary>
        /// <param name='type'>
        /// アルファブレンドタイプ
        /// </param>
        public void SetAlphaBlend( ALPHABLEND_TYPE type )
        {
            // タイプ別処理
            switch( type )
            {
            // ブレンドなし
            case ALPHABLEND_TYPE.NONE:
                {
                    m_GraphicsContext.Disable( EnableMode.Blend );
                }
                return;
            // 半透明
            case ALPHABLEND_TYPE.TRANS:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha );
                }
                break;
            // 加算
            case ALPHABLEND_TYPE.ADD:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.One, BlendFuncFactor.One );
                }
                break;
            // 半加算
            case ALPHABLEND_TYPE.TRANSADD:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.One );
                }
                break;
            // 減算
            case ALPHABLEND_TYPE.SUB:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.ReverseSubtract, BlendFuncFactor.Zero, BlendFuncFactor.OneMinusSrcAlpha );
                }
                break;
            // 乗算
            case ALPHABLEND_TYPE.MUL:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.Zero, BlendFuncFactor.SrcColor );
                }
                break;
            // 反転
            case ALPHABLEND_TYPE.REVERSE:
                {
                    m_GraphicsContext.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.OneMinusSrcColor, BlendFuncFactor.OneMinusSrcAlpha );
                }
                break;
            }
            
            // アルファブレンドの有効化
            m_GraphicsContext.Enable( EnableMode.Blend );
        }

        /// <summary>
        /// グラフィックスコンテキスト取得
        /// </summary>
        /// <value>
        /// グラフィックスコンテキスト
        /// </value>
        public GraphicsContext GraphicsContext
        {
            get { return m_GraphicsContext; }
        }
        
        /// <summary>
        /// スクリーンの横幅取得
        /// </summary>
        /// <value>
        /// スクリーンの横幅
        /// </value>
        public int ScreenWidth
        {
            get { return ( m_GraphicsContext != null ? m_GraphicsContext.Screen.Width : 0 ); }
        }
        
        /// <summary>
        /// スクリーンの縦幅取得
        /// </summary>
        /// <value>
        /// クリーンの縦幅
        /// </value>
        public int ScreenHeight
        {
            get { return ( m_GraphicsContext != null ? m_GraphicsContext.Screen.Height : 0 ); }
        }
        
        /// <summary>
        /// 白のテクスチャ取得
        /// </summary>
        /// <value>
        /// 白のテクスチャ
        /// サイズ1x1
        /// </value>
        public Texture2D WhiteTexture
        {
            get { return m_WhiteTexture; }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CGraphicsContext( )
        {
        }
        
        /// <summary>
        /// 標準クリアカラー
        /// </summary>
        private readonly Vector4            m_default_clear_color = new Vector4( 0.0f, 0.0f, 0.0f, 1.0f );
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CGraphicsContext     m_Instance          = null;
        
        /// <summary>
        /// グラフィックスコンテキスト
        /// </summary>
        private GraphicsContext             m_GraphicsContext   = null;
        
        /// <summary>
        /// 画面のクリアカラー
        /// </summary>
        private Vector4                     m_ClearColor        = Vector4.One;
        
        /// <summary>
        /// 白のテクスチャ
        /// </summary>
        private Texture2D                   m_WhiteTexture      = null;
    }
}
