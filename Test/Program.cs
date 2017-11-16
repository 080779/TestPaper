﻿using Chat.IService.Interface;
using Chat.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IAdminUserService adminService = new AdminUserService();
            IRoleService roleService = new RoleService();
            IPermissionService permissionService = new PermissionService();
            ITestPaperService paperService = new TestPaperService();
            IIdNameService idNameSevice = new IdNameService();

            //long[] ids = { 1, 2 };
            //long[] roleids = { 1 };
            //long[] addIds = { 2 };

            //permissionService.AddNew("manager", "管理员权限");
            //permissionService.AddNew("list", "查看列表权限");

            //long roleId = roleService.AddNew("系统管理员", "拥有所有权限");
            //long roleId1 = roleService.AddNew("普通账户", "拥有查看列表权限");

            //permissionService.AddPermissionIds(roleId, ids);
            //permissionService.AddPermissionIds(roleId1, addIds);

            //long id = adminService.AddAdminUser("system", "18318318383", true, "edfe@qq.com", "123456");

            //roleService.AddRoleIds(id, roleids);

            //Console.WriteLine(id);

            idNameSevice.AddNew("正确答案", "A",null);
            idNameSevice.AddNew("正确答案", "B", null);
            idNameSevice.AddNew("正确答案", "C", null);
            idNameSevice.AddNew("正确答案", "D", null);
            idNameSevice.AddNew("活动状态", "待开始", null);
            idNameSevice.AddNew("活动状态", "答题进行中", null);
            idNameSevice.AddNew("活动状态", "待开奖", null);
            long id= idNameSevice.AddNew("活动状态", "已结束", null);
            Console.WriteLine(id);

            //Console.WriteLine(paperService.Delete(3));
            Console.ReadKey();
        }
    }
}
