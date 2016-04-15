
/*!
 *  @file       GameFramework.cs
 *  @brief      ゲームフレームワーク
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/04/11
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using aqua.core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace aqua
{
    /// <summary>
    /// ゲームフレームワーククラス
    /// </summary>
    public sealed class CGameFramework
    {
        /// <summary>
        /// ゲームフレームワークのセットアップ
        /// </summary>
        /// <param name='game'>
        /// ゲームクラス
        /// </param>
        public static void Setup( IGame game )
        {
            // 起動メッセージ
            CDebug.WriteLine( "--- Welcome to AQUA GAME LIBRARY!! ---" );
            
            if( game == null ) return;
            
            // ゲームクラス設定
            m_Game = game;
            
            // 初期化
            Initialize( );
            
            // メインループ
            MainLoop( );
            
            // 解放
            Dispose( );
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize( )
        {
            // グラフィックスコンテキスト取得
            CGraphicsContext context = CGraphicsContext.GetInstance( );
            
            // グラフィックスコンテキスト初期化
            context.Initialize( );
            
            // タッチ処理初期化
            CTouch.Initialize( );
            
            // フレーム制御クラス生成
            m_FrameSync = new CFrameSync( );
            
#if DEBUG
            // パフォーマンス測定クラス生成
            m_Performance = new CPerformance( );
#endif
            // スプライトマネージャ初期化
            CSpriteManager.GetInstance( ).Initialize( );
            
            // ゲームクラス初期化
            m_Game.Initialize( );
        }
        
        /// <summary>
        /// メインループ
        /// </summary>
        public static void MainLoop( )
        {
            // グラフィックスコンテキスト取得
            CGraphicsContext context = CGraphicsContext.GetInstance( );
            
            // メインループ
            while( m_Exit == false )
            {
#if DEBUG
                // 測定開始
                m_Performance.Begin( );
#endif
                // システムイベントチェック
                SystemEvents.CheckEvents( );
                
                // フレーム制御
                if( m_FrameSync.Sync( ) == true )
                {
                    // ボタン入力更新
                    CGamePad.Update( );
                    
                    // タッチ入力更新
                    CTouch.Update( );
                    
                    // モーション入力
//                    CMotion.Update( );
                    
                    // サウンド再生終了チェック
                    CSound.CheckEnd( );
#if DEBUG
                    // FPS計測
                    m_Performance.CalcFPS( );
                    
                    // ゲームアップデート測定用ラベル追加
                    m_Performance.AddLabel( "System Update" );
#endif
                    // ゲーム更新
                    m_Game.Update( );
                    
#if DEBUG
                    // ゲームアップデート測定用ラベル追加
                    m_Performance.AddLabel( "Game Update" );
#endif
                    // スプライトマネージャ更新
                    CSpriteManager.GetInstance( ).Update( );
                    
#if DEBUG
                    // 2D更新測定用ラベル追加
                    m_Performance.AddLabel( "2D Update" );
#endif
                    // 画面クリア
                    ScreenClear( );
                    
                    // スプライトマネージャ描画
                    CSpriteManager.GetInstance( ).Draw( );
#if DEBUG
                    // 2D描画測定用ラベル追加
                    m_Performance.AddLabel( "2D Draw" );
                    
                    // 測定値更新
                    m_Performance.End( );
                    
                    // デバッグフォント描画
                    CDebugFont.GetInstance( ).Draw( );
#endif
                    // バックバッファ転送
                    context.SwapBuffer( );
#if DEBUG
                    // パフォーマンス情報表示切替
                    if( CGamePad.Trigger( GamePadButtons.Select ) )
                        m_Performance.visible = !m_Performance.visible;
#endif
                    // ガベージコレクション
                    GC.Collect( );
                    
                    // システムメモリダンプ
//                    SystemMemory.Dump( );
                }
                
                // 空いているスレッドに切り替え
                System.Threading.Thread.Yield( );
            }
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public static void Dispose( )
        {
            // ゲーム解放
            m_Game.Dispose( );
            
#if DEBUG
            // パフォーマンス測定クラス解放
            m_Performance.Dispose( );
#endif
            
            // BGM解放
            CBgm.Unload( );
            
            CSpriteManager.GetInstance( ).Dispose( );
            
            // デバッグフォント解放
            CDebugFont.GetInstance( ).Dispose( );
            
            // グラフィックスコンテキスト解放
            CGraphicsContext.GetInstance( ).Dispose( );
        }
        
        /// <summary>
        /// 画面のクリア
        /// </summary>
        public static void ScreenClear( )
        {
            CGraphicsContext.GetInstance( ).Clear( );
        }
        
        /// <summary>
        /// スクリーンの横幅取得
        /// </summary>
        /// <value>
        /// スクリーンの横幅
        /// </value>
        public static int ScreenWidth
        {
            get { return CGraphicsContext.GetInstance( ).ScreenWidth; }
        }
        
        /// <summary>
        /// スクリーンの縦幅取得
        /// </summary>
        /// <value>
        /// スクリーンの縦幅
        /// </value>
        public static int ScreenHeight
        {
            get { return CGraphicsContext.GetInstance( ).ScreenHeight; }
        }
        
        /// <summary>
        /// スクリーンのアスペクト比取得
        /// </summary>
        /// <value>
        /// スクリーンのアスペクト比
        /// </value>
        public static float ScreenAspectRatio
        {
            get { return CGraphicsContext.GetInstance( ).GraphicsContext.Screen.AspectRatio; }
        }
        
        /// <summary>
        /// フレームレート取得と設定
        /// </summary>
        /// <value>
        /// フレームレート
        /// </value>
        public static int FrameRate
        {
            get { return m_FrameSync.FrameRate;  }
            set { m_FrameSync.FrameRate = value; }
        }
        
        /// <summary>
        /// アプリケーション終了フラグ
        /// </summary>
        private static bool             m_Exit          = false;
        
        /// <summary>
        /// ゲームクラス
        /// </summary>
        private static IGame            m_Game          = null;
        
        /// <summary>
        /// フレーム制御クラス
        /// </summary>
        private static CFrameSync       m_FrameSync     = null;
        
#if DEBUG
        /// <summary>
        /// パフォーマンス計測クラス
        /// </summary>
        private static CPerformance     m_Performance   = null;
#endif
    }
}
