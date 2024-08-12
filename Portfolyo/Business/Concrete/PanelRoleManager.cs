using Business.Abstract;
using Business.Contants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PanelRoleManager : IPanelRoleService
    {
        private IPanelRoleDal _panelRoleDal;

        public PanelRoleManager(IPanelRoleDal panelRoleDal)
        {
            _panelRoleDal = panelRoleDal;
        }

        public IDataResult<List<PanelRole>> TGetList(Expression<Func<PanelRole, bool>> filter = null)
        {
            var result = _panelRoleDal.GetDataList(filter);

            return new SuccessDataResult<List<PanelRole>>(result, Messages.QuerySuccess);
        }

        public IEnumerable<string> TGetListForRoleName(Expression<Func<PanelRole, bool>> filter = null)
        {
            var result = _panelRoleDal.GetDataList(filter).Select(r => r.RoleName);

            return result;
        }

        public IDataResult<PanelRole> GetById(int id)
        {
            var result = _panelRoleDal.GetData(pr => pr.Id == id);

            return new SuccessDataResult<PanelRole>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(PanelRole entity)
        {
            _panelRoleDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _panelRoleDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(PanelRole entity)
        {
            _panelRoleDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
