namespace Domain.Common;

public interface IRepository<out T>
    where T : IAgreggateRoot;