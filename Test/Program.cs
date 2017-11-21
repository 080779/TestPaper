using Chat.DTO.DTO;
using Chat.IService.Interface;
using Chat.Service.Service;
using Chat.WebCommon;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IActivityService actService = new ActivityService();
            //bool b= actService.CheckByStatusId(19, 6);
            bool b = actService.ExistActivity(13);
            string m= CommonHelper.FormatMoblie("15615615656");
            Console.WriteLine(b);
            Console.ReadKey();
        }

        static void Main2(string[] args)
        {
            IUserService userService = new UserService();
            UserDTO[] dtos = userService.GetAll();
            int count = dtos.Count();
            IWorkbook wb1 = new XSSFWorkbook();
            ISheet sheet1 = wb1.CreateSheet();
            IRow Row1= sheet1.CreateRow(0); 

            ICell cell0 = Row1.CreateCell(0);
            cell0.SetCellValue("编号");

            ICell cell1 = Row1.CreateCell(1);
            cell1.SetCellValue("昵称");

            ICell cell2 = Row1.CreateCell(2);
            cell2.SetCellValue("姓名");

            ICell cell3 = Row1.CreateCell(3);
            cell3.SetCellValue("手机号");

            ICell cell4 = Row1.CreateCell(4);
            cell4.SetCellValue("联系地址");

            ICell cell5 = Row1.CreateCell(5);
            cell5.SetCellValue("参与活动次数");

            ICell cell6 = Row1.CreateCell(6);
            cell6.SetCellValue("中奖次数");
            int i = 1;
            foreach(var dto in dtos)
            {
                Row1 = sheet1.CreateRow(i++);
                Row1.CreateCell(0).SetCellValue(dto.Id);
                Row1.CreateCell(1).SetCellValue(dto.NickName);
                Row1.CreateCell(2).SetCellValue(dto.Name);
                Row1.CreateCell(3).SetCellValue(dto.Mobile);
                Row1.CreateCell(4).SetCellValue(dto.Address);
                Row1.CreateCell(5).SetCellValue(dto.PassCount);
                Row1.CreateCell(6).SetCellValue(dto.WinCount);
            }


            //foreach(var dto in dtos)
            //{
            //    Row1 = sheet1.CreateRow(i);
            //    for(int j=0;j<propCount;j++)
            //    {
            //        infos[count].GetValue(dto);
            //    }
            //}

            //IWorkbook wb = new XSSFWorkbook();
            //ISheet sheet = wb.CreateSheet();
            //IRow row = sheet.CreateRow(0);
            //ICell cell0 = row.CreateCell(0);
            //cell0.SetCellValue(true);
            //ICell cell1 = row.CreateCell(1);
            //cell1.SetCellValue(111);
            //ICell cell2 = row.CreateCell(2);
            //cell2.SetCellValue(DateTime.Now);
            //ICell cell3 = row.CreateCell(3);
            //cell3.SetCellValue(2.165);
            //row = sheet.CreateRow(1);
            //cell0 = row.CreateCell(0);
            //cell0.SetCellValue(false);
            //row.CreateCell(1);
            //cell1.SetCellValue(111);
            //cell2 = row.CreateCell(2);
            //cell2.SetCellValue(DateTime.Now);
            //cell3 = row.CreateCell(3);
            //cell3.SetCellValue(2.165);

            using (FileStream stream = File.OpenWrite(@"F:\temp\aaa.xlsx"))
            {
                wb1.Write(stream);
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
        static void Main3(string[] args)
        {
            IAdminUserService adminService = new AdminUserService();
            IRoleService roleService = new RoleService();
            IPermissionService permissionService = new PermissionService();
            ITestPaperService paperService = new TestPaperService();
            IIdNameService idNameSevice = new IdNameService();
            IUserService userService = new UserService();
            IActivityService actService = new ActivityService();

            //long[] ids = { 1, 2 };
            long[] roleids = { 1 };
            //long[] addIds = { 2 };

            //permissionService.AddNew("manager", "管理员权限");
            //permissionService.AddNew("list", "查看列表权限");

            //long roleId = roleService.AddNew("系统管理员", "拥有所有权限");
            //long roleId1 = roleService.AddNew("普通账户", "拥有查看列表权限");

            //permissionService.AddPermissionIds(roleId, ids);
            //permissionService.AddPermissionIds(roleId1, addIds);

            //long id = adminService.AddAdminUser("fsddf", "15615615656", true, "edfe@qq.com", "1");

            //roleService.AddRoleIds(id, roleids);

            //Console.WriteLine(id);

            //idNameSevice.AddNew("正确答案", "A",null);
            //idNameSevice.AddNew("正确答案", "B", null);
            //idNameSevice.AddNew("正确答案", "C", null);
            //idNameSevice.AddNew("正确答案", "D", null);
            //idNameSevice.AddNew("活动状态", "待开始", null);
            //idNameSevice.AddNew("活动状态", "答题进行中", null);
            //idNameSevice.AddNew("活动状态", "待开奖", null);
            //long id= idNameSevice.AddNew("活动状态", "已结束", null);
            //long id = userService.AddNew("xh313", "", "", "154186147455", false, "rqqewqeq");
            //actService.AddUserId(11, 2);
            //Console.WriteLine(actService.AddUserId(11,1));

            //userService.e

            //Console.WriteLine(paperService.Delete(3));

            long c= actService.GetActivityByUserId(3).Count();
            Console.WriteLine(c);
            Console.ReadKey();
        }
    }
}
