using TaxiApplication.Domain.Entity;

namespace TaxiApplication.BLL.Implementations;

public class ClientService : IClientService
{
	private readonly IUnitOfWork _unitOfWork;

	public ClientService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IBaseResponse<Client>> AddClient(Client Client)
	{
		try
		{

			var RealClient = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(c => c.Login.Trim().ToLower() == Client.Login.Trim().ToLower());
			if (RealClient is not null)
			{
				return new BaseResponse<Client>()
				{
					Description = "Пользователь с таким логином уже сущевствует.",
					StatusCode = StatusCode.UserAlredyExist
				};
			}

			await _unitOfWork.ClientRepository.AddAsync(Client);

			return new BaseResponse<Client>()
			{
				Data = Client,
				Description = "Пользователь добавлен.",
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientService.AddClient] error: {ex.Message})");
			return new BaseResponse<Client>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}


	}

	public async Task<IBaseResponse<bool>> DeleteClient(int id)
	{
		try
		{
			var RealClient = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(x => x.Id == id);
			if (RealClient is null)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = "Пользователь не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			await _unitOfWork.ClientRepository.DeleteAsync(RealClient);

			return new BaseResponse<bool>()
			{
				Data = true,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientService.DeleteClient] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<Client>> FirstOrDefault(Func<Client, bool> filter)
	{
		try
		{
			var RealClient = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(filter);
			if (RealClient is null)
			{
				return new BaseResponse<Client>()
				{
					Description = "Пользователь не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			var profile = await _unitOfWork.ClientProfileRepository.FirstOrDefaultAsync(cl => cl.ClientId == RealClient.Id);

			if (profile == null)
			{
				return new BaseResponse<Client>()
				{
					StatusCode = StatusCode.AllError,
					Description = $"Пользователь с таким профилем не найден."
				};
			}
			RealClient.Profile = profile;


			return new BaseResponse<Client>()
			{
				Data = RealClient,
				Description = "Пользователь найден.",
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientService.FirstOrDefault] error: {ex.Message})");
			return new BaseResponse<Client>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};

		}
	}

	public async Task<IBaseResponse<IEnumerable<Client>>> GetAllClients()
	{
		try
		{
			var AllClients = await _unitOfWork.ClientRepository.ListAllAsync();

			if (AllClients is null)
			{
				return new BaseResponse<IEnumerable<Client>>()
				{
					Description = "Пользователи отсутсвуют.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			foreach (var client in AllClients)
			{
				var profile = await _unitOfWork.ClientProfileRepository.FirstOrDefaultAsync(cl => cl.ClientId == client.Id);

				if (profile == null)
				{
					return new BaseResponse<IEnumerable<Client>>()
					{
						StatusCode = StatusCode.AllError,
						Description = $"Пользователь с таким профилем не найден."
					};
				}
				client.Profile = profile;
			}

			return new BaseResponse<IEnumerable<Client>>()
			{ 
				Data = AllClients,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientService.GetAllClients] error: {ex.Message})");
			return new BaseResponse<IEnumerable<Client>>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<Client>> GetClient(int id)
	{
		try
		{
			var RealClient = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(x => x.Id == id);
			if (RealClient is null)
			{
				return new BaseResponse<Client>()
				{
					Description = "Пользователь отсутсвует.",
					StatusCode = StatusCode.UserNotFound
                };
            }

            var profile = await _unitOfWork.ClientProfileRepository.FirstOrDefaultAsync(cl => cl.ClientId == RealClient.Id);

            if (profile == null)
            {
                return new BaseResponse<Client>()
                {
                    StatusCode = StatusCode.AllError,
                    Description = $"Пользователь с таким профилем не найден."
                };
            }
            RealClient.Profile = profile;

            return new BaseResponse<Client>()
			{
				Data = RealClient,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[ClientService.GetAllClients] error: {ex.Message})");
			return new BaseResponse<Client>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

    public async Task<IBaseResponse<bool>> UpdateClient(Client Client)
    {
        try
        {
            var RealClient = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(x => x.Id == Client.Id);
            if (RealClient is null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Пользователь отсутсвует.",
                    StatusCode = StatusCode.UserNotFound
                };
            }

			RealClient.Profile = Client.Profile;
			RealClient.Login = Client.Login;
			RealClient.Password = Client.Password;

			await _unitOfWork.ClientRepository.UpdateAsync(RealClient);

			return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[ClientService.GetAllClients] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				Data = false,
                StatusCode = StatusCode.AllError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}
