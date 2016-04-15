
/*!
 *  @file       Model.cs
 *  @brief      モデル
 *  @author     Kazuya Maruyama
 *  @date       2014/01/05
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.Model;
using System.Diagnostics;

namespace aqua
{
    /// <summary>
    /// モデルクラス
    /// </summary>
    public class CModel
        : aqua.core.IDrawObject3D, IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CModel( )
        {
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='file_name'>
        /// ファイル名
        /// </param>
        /// <param name='camera'>
        /// カメラ
        /// </param>
        /// <param name='light'>
        /// ライト
        /// </param>
        public CModel( string file_name, string shader_name, CCamera3D camera, CLight light )
        {
            Create( file_name, shader_name, camera, light );
        }
        
        /// <summary>
        /// モデル生成
        /// </summary>
        /// <param name='file_name'>
        /// ファイル名
        /// </param>
        /// <param name='camera'>
        /// カメラ
        /// </param>
        /// <param name='light'>
        /// ライト
        /// </param>
        /// <param name='material'>
        /// マテリアル
        /// </param>
        public void Create( string file_name, string shader_name, CCamera3D camera, CLight light )
        {
            m_FileName      = file_name;
            
            // カメラ保存
            m_Camera        = camera;
            
            // ライト保持
            m_Light         = light;
            
            // マテリアル生成
            m_Material      = new CMaterial( );
            
            // モデル生成
            m_BasicModel    = new BasicModel( "/Application/" + m_FileName, 0 );
            
            // シェーダプラグラム生成
            m_BasicProgram  = new BasicProgram( "/Application/" + shader_name );
            
            // ユニフォーム変数追加
            m_BasicProgram.SetUniformBinding( m_append_uniform_over_diffuse_id, m_append_uniform_over_diffuse_name );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
            // トランスフォーム更新
            UpdateTransform( );
            
            // モデル更新
            m_BasicModel.Update( );
        }
        
        /// <summary>
        /// 描画
        /// </summary>
        public override void Draw( )
        {
            // グラフィックスコンテキスト取得
            CGraphicsContext context = CGraphicsContext.GetInstance( );
            
            // シェーダプログラム取得
            BasicParameters param = m_BasicProgram.Parameters;
            
            // ライト情報を設定
            Vector3 light_diffuse = m_Light.diffuse_color.Xyz;
            
            param.SetLightCount( 1 );
            param.SetLightDiffuse( 0, ref light_diffuse );
            param.SetLightDirection( 0, ref m_Light.direction );
            
            // 色設定
            m_BasicProgram.SetUniformValue( m_append_uniform_over_diffuse_id, ref m_Material.diffuse_color );
            
            // カメラ行列取得
            Matrix4 view        = m_Camera.ViewMatrix;
            Matrix4 projection  = m_Camera.ProjectionMatrix;
            
            // シェーダにカメラ設定
            param.SetViewMatrix( ref view );
            param.SetProjectionMatrix( ref projection );
            
            // シェーダ設定
            m_BasicProgram.Parameters = param;
            
            // トランスフォーム行列設定
            m_BasicModel.SetWorldMatrix( ref m_Transform );
            
            // アルファブレンド設定
            context.SetAlphaBlend( Alphablend );
            
            // カリング設定
            context.GraphicsContext.Enable( EnableMode.CullFace );
            context.GraphicsContext.SetCullFace( CullFaceMode.Back, CullFaceDirection.Ccw );
            
            // 深度テスト
            context.GraphicsContext.Enable( EnableMode.DepthTest );
            context.GraphicsContext.SetDepthFunc( DepthFuncMode.LEqual, true );
            
            // モデル描画
            m_BasicModel.Draw( CGraphicsContext.GetInstance( ).GraphicsContext, m_BasicProgram );
            
            context.GraphicsContext.Disable( EnableMode.CullFace );
            context.GraphicsContext.Disable( EnableMode.DepthTest );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public virtual void Dispose( )
        {
            m_BasicProgram.Dispose( );
            
            m_BasicModel.Dispose( );
        }
        
        /// <summary>
        /// カメラの取得と設定
        /// </summary>
        /// <value>
        /// 3Dカメラ
        /// </value>
        public CCamera3D Camera
        {
            get { return m_Camera;  }
            set { m_Camera = value; }
        }
        
        /// <summary>
        /// ライトの取得と設定
        /// </summary>
        /// <value>
        /// ライト
        /// </value>
        public CLight Light
        {
            get { return m_Light;  }
            set { m_Light = value; }
        }
        
        /// <summary>
        /// マテリアルの取得と設定
        /// </summary>
        /// <value>
        /// マテリアル
        /// </value>
        public CMaterial Material
        {
            get { return m_Material;  }
            set { m_Material = value; }
        }
        
        /// <summary>
        /// ディフューズカラーの取得と設定
        /// </summary>
        /// <value>
        /// ディフューズカラー
        /// </value>
        public Vector4 Diffuse
        {
            get { return m_Material.diffuse_color;  }
            set { m_Material.diffuse_color = value; }
        }
        
        /// <summary>
        /// 追加ユニフォーム変数のID
        /// </summary>
        private const int       m_append_uniform_over_diffuse_id   = 19;
        
        /// <summary>
        /// 追加ユニフォーム変数の名前
        /// </summary>
        private const string    m_append_uniform_over_diffuse_name = "OverDiffuse";
        
        /// <summary>
        /// ファイル名
        /// </summary>
        private string          m_FileName;
        
        /// <summary>
        /// モデルクラス
        /// </summary>
        private BasicModel      m_BasicModel    = null;
        
        /// <summary>
        /// シェーダプログラムクラス
        /// </summary>
        private BasicProgram    m_BasicProgram  = null;
        
        /// <summary>
        /// カメラクラス
        /// </summary>
        private CCamera3D       m_Camera        = null;
        
        /// <summary>
        /// ライトクラス
        /// </summary>
        private CLight          m_Light         = null;
        
        /// <summary>
        /// マテリアルクラス
        /// </summary>
        private CMaterial       m_Material      = null;
    }
}
