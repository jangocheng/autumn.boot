using System;
using AutoMapper;
using Autumn.Model;
using Autumn.Services.BASE;
using Autumn.IServices;
using System.Threading.Tasks;
using Autumn.IRepository;
using Autumn.FrameWork;

namespace Autumn.Services
{
    /// <summary>
    /// �û�
    /// </summary>	
    public class UserService : BaseService<S01_User>, IUserService
    {
        /// <summary>
        /// ƥ��Entity�е��ֶ�
        /// </summary>
        IMapper _mapper;

        /// <summary>
        /// ��Ҫʹ�������������ڴ˹���ʱע��
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository dal, IMapper mapper)
        {
            BaseDal = dal;
            _mapper = mapper;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="userCode">�û���</param>
        /// <param name="Password">����</param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(string userCode, string Password="")
        {
            UserModel userModel = new UserModel();

            var demoList = await Query(a => a.S01_UserCode == userCode && a.S01_Password == Password);

            if (demoList.Count > 0)
            {
                //userModel = _mapper.Map<UserModel>(demoList);
                userModel.UserId = demoList[0].S01_UserId;
                userModel.UserName = demoList[0].S01_UserName;
                userModel.RoleIds = demoList[0].S02_RoleIds;
                //userModel.Urls = "user/getuser|user/getrole";
            }
            return userModel;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> InsertUser(UserModel userModel)
        {
            S01_User user = new S01_User();
            user.S01_UserCode = userModel.UserCode;
            user.S01_Password = userModel.UserName;
            user.S01_IsValid = 0;
            user.S01_CreateId = 9;
            user.S01_CreateBy = "user";
            user.S01_CreateTime = DateTime.Now;
            return await Insert(user);
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserModel userModel)
        {
            S01_User user = new S01_User();
            user.S01_Remarks = "��ע";
            return await Update(user);
        }

        /// <summary>
        /// �����ύ
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<bool> TransExecuteUser(UserModel userModel)
        {
            Func<Task<bool>> action = async () =>
            {
                S01_User user = new S01_User();
                user.S01_Remarks = "��ע2";
                return await Update(user);
            };

            action += async () =>
            {
                S01_User user = new S01_User();
                user.S01_UserCode = userModel.UserCode;
                user.S01_Password = userModel.UserName;
                user.S01_IsValid = 0;
                user.S01_CreateId = 9;
                user.S01_CreateBy = "user";
                user.S01_CreateTime = DateTime.Now;
                return await Insert(user) > 0 ? true : false;
            };

            return await ActionTran(action);
        }

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        private void TestDemo()
        {
            // ԭʼSQL���
            //var dt = Db.Ado.GetDataTable("select * from table where id=@id and name=@name", new List<DbLiteParameter>(){
            //new DbLiteParameter("@id",1),
            //new DbLiteParameter("@name",2)
            //});

            // TOP20
            //var top20 = Db.Queryable<Student>().Take(20).ToList();

            // ǰ20ʣ������
            //var skip20 = db.Queryable<Student>().Skip(20).ToList();

            // �����ѯ���ҷ���d��d1���������󼯺�
            //var list = Db.Queryable<Demo, Demo1, Demo2>((d, d1, d2) => new object[] {
            //JoinType.Left,d.Id==d1.Id,
            //JoinType.Left,d1.Id2==d2.Id
            // })
            //.Where((d, d1, d2) => d2.Id == 1 || d1.Id == 1 || d.Id == 1)
            //.OrderBy((d) => d.Id)
            //.OrderBy((d, d1) => d.Name, OrderByType.Desc)
            //.Select((d, d1, d2) => new { d = d, d1 = d1 }).ToList();

            // �����ѯ
            //var group = Db.Queryable<Student>().GroupBy(it => it.Id)
            //.Having(it => SqlFunc.AggregateCount(it.Id) > 10)
            //.Select(it => new { id = SqlFunc.AggregateCount(it.Id) }).ToList();

            // ȥ��
            //var list3 = Db.Queryable<Student>()
            //.GroupBy(it => new { it.Id, it.Name }).Select(it => new { it.id, it.Name }).ToList();
            //Ч���������� select distinct id,name from student

            // �Զ������������Զ�����
            //var update = Db.Updateable(updateObj).UpdateColumns(s => new { s.RowStatus, s.Id }).WhereColumns(it => new { it.Id });

            //���ݲ�ͬ����ִ�и��²�ͬ����
            //var t3 = Db.Updateable(updateObj)
            //.UpdateColumnsIF(caseValue == "1", it => new { it.Name })
            //.UpdateColumnsIF(caseValue == "2", it => new { it.Name, it.CreateTime })
            //.ExecuteCommand();

            // ������޸� ���������ж��Ƿ���ڣ������������£�����������룬֧����������
            //�������ڿ�����
            //Db.Saveable<Student>(entity).ExecuteReturnEntity();
            //Ч�� UPDATE [STudent]  SET
            //[SchoolId]=@SchoolId,[Name]=@Name,[CreateTime]=@CreateTime WHERE[Id] = @Id 

            //���������ڿ�����
            //Db.Saveable<Student>(new Student() { Name = "" }).ExecuteReturnEntity();
            //Ч�� INSERT INTO[STudent]
            //([SchoolId],[Name],[CreateTime])
            // VALUES
            //(@SchoolId, @Name, @CreateTime); SELECT SCOPE_IDENTITY();

            //�������ò�����˺�ָ����
            //Db.Saveable<Student>(new Student() { Name = "" }).InsertColumns(it => it.Name).ExecuteReturnEntity();
            //Db.Saveable<Student>(new Student() { Name = "" }).InsertIgnoreColumns(it => it.SchoolId).ExecuteReturnEntity();

            //Ҳ�������ø��¹��˺�ָ����
            //Db.Saveable<Student>(entity).UpdateIgnoreColumns(it => it.SchoolId).ExecuteReturnEntity();
            //Db.Saveable<Student>(entity).UpdateColumns(it => new { it.Name, it.CreateTime }).ExecuteReturnEntity();

            //���벢������������ExecuteReutrnIdentity
            //int t30 = Db.Insertable(insertObj).ExecuteReturnIdentity();
            //long t31 = Db.Insertable(insertObj).ExecuteReturnBigIdentity();

            // ��Ч��������
            //var insertObjs = new List<Student>();
            //var s9 = Db.Insertable(insertObjs.ToArray()).ExecuteCommand();

            //�洢����
            //var dt2 = Db.Ado.UseStoredProcedure().GetDataTable("sp_school", new { name = "����", age = 0 });//  GetInt SqlQuery<T>  �ȵȶ�������
            ////֧��output
            //var nameP = new DbLiteParameter("@name", "����");
            //var ageP = new DbLiteParameter("@age", null, true);//isOutput=true
            //var dt2 = Db.Ado.UseStoredProcedure().GetDataTable("sp_school", nameP, ageP);
            ////ageP.value�����õ�����ֵ

            //SQL����
            //var sql = Db.Queryable<T>().ToSql();
        }
    }
}
