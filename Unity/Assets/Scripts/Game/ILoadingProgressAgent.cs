namespace Game
{
    public interface ILoadingProgressAgent
    {
		//总的进度=每个小进度自身在总进度里的权重，以及每个小进度逻辑处自身的加载进度

        /// <summary>
        /// 权重
        /// </summary>
        int Weight{get;}

        /// <summary>
        /// 当前进度，约定为[0, 1]
        /// 只在系统切换阶段有效
        /// </summary>
        float Progress{get;}
        
    }
}