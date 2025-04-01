using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TesteTécnicoIdeal.WPF.DTO_s;
using TesteTécnicoIdeal.WPF.Models;
using static System.Net.WebRequestMethods;

namespace TesteTécnicoIdeal.WPF.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ApiService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<User_Model>> GetAllUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("user");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao buscar cadastro: {errorMessage}");
                }

                var users = await response.Content.ReadFromJsonAsync<List<User_Model>>();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<User_Model> GetUserById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao buscar cadastro: {errorMessage}");
                }

                var user = await response.Content.ReadFromJsonAsync<User_Model>();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task CreateUser(UserDTO userDTO)
        {
            var mappedUser = _mapper.Map<User_Model>(userDTO);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("user", mappedUser);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao realizar cadastro: {errorMessage}");
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task UpdateUser(User_Model user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"user/{user.Id}", user);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao atualizar cadastro: {errorMessage}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task DeleteUser(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"user/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao deletar cadastro: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
