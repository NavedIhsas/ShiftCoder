using System.Collections.Generic;
using _0_FrameWork.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Permission = AccountManagement.Domain.RoleAgg.Permission;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateRoleViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var role = new Role(command.Name, new List<Permission>());
            _repository.Create(role);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRoleViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var getRole = _repository.GetById(command.Id);
            if (getRole == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            var permissions = new List<Permission>();
            command.Permissions.ForEach(code => permissions.Add(new Permission(code)));

            getRole.Edit(command.Name, permissions);
            _repository.Update(getRole);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditRoleViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<RoleViewModel> GetAll()
        {
            return _repository.GetAllList();
        }
    }
}