﻿namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public Permission(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public Permission()
        {
            
        }
        public Permission(int code)
        {
            Code = code;
        }

        public long Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
    }
}