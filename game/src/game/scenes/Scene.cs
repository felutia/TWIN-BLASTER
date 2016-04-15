
/*!
 *  @file       Scene.cs
 *  @brief      シーン
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// シーンベースクラス
    /// </summary>
    public abstract class IScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IScene( )
        {
        }
		
		public virtual void Initialize( )
		{
		}
		
		public virtual void Update()
		{
		}
		
		public virtual void Dispose()
		{
		}
    }
}
