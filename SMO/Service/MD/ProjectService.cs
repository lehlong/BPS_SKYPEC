using iTextSharp.text;
using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Service.MD
{
    public class ProjectService : GenericService<T_MD_PROJECT, ProjectRepo>
    {
        public ProjectService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (this.ObjDetail.YEAR == 0)
                {
                    this.State = false;
                    this.ErrorMessage = "Vui lòng nhập năm dự án";
                    return;
                }
                var lstProjects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == this.ObjDetail.YEAR).Select(x => x.CODE).ToList();
                List<int> lstValue = new List<int>();
                foreach(var i in lstProjects)
                {
                    var endCode = Convert.ToInt32(i.Replace("DA-", "").Replace($"{this.ObjDetail.YEAR}-", ""));
                    lstValue.Add(endCode);
                }
                this.ObjDetail.CODE = lstValue.Count() == 0 ? $"DA-{this.ObjDetail.YEAR}-1" : $"DA-{this.ObjDetail.YEAR}-{lstValue.Max() + 1}";
                base.Create();
            }
            catch (Exception ex)
            {
                State = false;
                Exception = ex;
            }
        }
    }
}
