using System;

namespace _0_FrameWork.Application
{
    public class NameGenerator
    {
        public static string UniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}