﻿using Assets.Data.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Runtime;
using System.Text.Json;

namespace Assets.Data.DataAccess;

public class MyDb : IMyDb
{
    string GetVal(string val) => _config.GetSection("Oracle")[val]!;
    string OracleConnString => $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={GetVal("host")})(PORT={GetVal("port")}))(CONNECT_DATA=(SID={GetVal("sid")})));User Id={GetVal("user")};Password={GetVal("password")};";
    public async Task<ArpUser?> GetOracleUser(int idNo)
    {
        string query = $"select * from buss_prod.vu_talabat  where ID_NO={idNo}";
        ArpUser? user = null;
        using (OracleConnection con = new OracleConnection(OracleConnString))
        using (var cmd = con.CreateCommand())
        {
            await con.OpenAsync();
            cmd.CommandText = query;
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    user = new ArpUser();
                    user.id = reader.GetInt32(0);
                    user.IdNo = reader.GetString(1);
                    user.name = reader.GetString(2);
                    user.DepartmentId = reader.GetString(3);
                    user.DepartmentName = reader.GetString(4);
                    user.ManagerId = reader.GetValue(5) == DBNull.Value ? 0 : reader.GetInt32(5);
                    user.Title = reader.GetString(6);
                    user.Email = reader.GetValue(7) == DBNull.Value ? "" : reader.GetString(7);
                    user.Phone = reader.GetValue(8) == DBNull.Value ? "" : reader.GetString(8);
                    user.IsManager = reader.GetInt32(9) == 1 ? true : false;
                    user.DepCode2 = reader.GetString(10);
                    //user.DepName2 = reader.GetString(11);
                    user.DepManager2 = reader.GetInt32(12);
                }
            }
            if (user != null)
            {
                int mid = user.IsManager ? user.DepManager2 : user.ManagerId;
                cmd.CommandText = ManagerQuery(mid);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var manager = new ArpUser();
                        manager.id = reader.GetInt32(0);
                        manager.IdNo = reader.GetString(1);
                        manager.name = reader.GetString(2);
                        manager.DepartmentId = reader.GetString(3);
                        manager.DepartmentName = reader.GetString(4);
                        manager.ManagerId = reader.GetValue(5) == DBNull.Value ? 0 : reader.GetInt32(5);
                        manager.Title = reader.GetString(6);
                        manager.Email = reader.GetValue(7) == DBNull.Value ? "" : reader.GetString(7);
                        manager.Phone = reader.GetValue(8) == DBNull.Value ? "" : reader.GetString(8);
                        manager.IsManager = reader.GetInt32(9) == 1 ? true : false;
                        manager.DepCode2 = reader.GetString(10);
                        //manager.DepName2 = reader.GetString(11);
                        manager.DepManager2 = reader.GetInt32(12);
                        user.Manager = manager;
                    }
                }
            }
        }
        //var json = JsonSerializer.Serialize(user);
        return user;
    }
    string ManagerQuery(int managerId)
    {
        return @$"select * from buss_prod.vu_talabat where Empl_Code={managerId}";
    }
    public async Task<List<string>> GetIds()
    {
        List<string> ids = new List<string>();
        using (OracleConnection con = new OracleConnection(OracleConnString))
        using (var cmd = con.CreateCommand())
        {
            await con.OpenAsync();
            cmd.CommandText = "select ID_NO from buss_prod.vu_talabat";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    ids.Add(reader.GetString(0));
                }
            }
        }
        return ids;
    }

    public async Task UpdateArpUser(ArpUser user)
    {
        using (var cmd = MyCommand.CmdProc("ArpUser_Update", connectionString))
        {
            var json = MyCommand.ToJson(user);
            //if (string.IsNullOrWhiteSpace(json) || json.Length<5) return;
            cmd.Parameters.AddWithValue("@json", json);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    public async Task<ArpUser?> GetArpUserTreeByUser(int userId)
    {

        using (var cmd = MyCommand.CmdProc("ArpUserDetailsByIdNo", connectionString))
        {
            cmd.Parameters.AddWithValue("@idno", userId);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                var json = await MyCommand.GetJson(reader);
                if (!string.IsNullOrWhiteSpace(json.ToString()))
                {
                    return JsonSerializer.Deserialize<ArpUser>(json);
                }
                else
                    return null;
            }
        }

    }
    public async Task UpdateLoginHistory(int loginType, string machineName, string loginId, int statusId)
    {
        using (var cmd = MyCommand.CmdProc("LoginHistoryUpdate", connectionString))
        {
            cmd.Parameters.AddWithValue("@LoginType", loginType);
            cmd.Parameters.AddWithValue("@MachineName", machineName);
            cmd.Parameters.AddWithValue("@LoginId", loginId);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    public async Task<ArpUser?> Login(int userId, string password)
    {
        using (var cmd = MyCommand.CmdProc("ArpUserLogin", connectionString))
        {
            cmd.Parameters.AddWithValue("@userName", userId);
            cmd.Parameters.AddWithValue("@password", password);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                var json = await MyCommand.GetJson(reader);
                if (!string.IsNullOrWhiteSpace(json.ToString()))
                {
                    return JsonSerializer.Deserialize<ArpUser>(json);
                }
            }
        }
        return null;
    }
    public async Task<List<AssetDelivery>?> AssetDeliveriesByBeneficiary(int userId, int beneficiaryId)
    {
        using (var cmd = MyCommand.CmdProc("AssetDeliveriesByBeneficiary", connectionString))
        {
            cmd.Parameters.AddWithValue("@BeneficiaryId", beneficiaryId);
            cmd.Parameters.AddWithValue("@userId", userId);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                var json = await MyCommand.GetJson(reader);
                if (!string.IsNullOrWhiteSpace(json.ToString()))
                {
                    return JsonSerializer.Deserialize<List<AssetDelivery>>(json);
                }
            }
        }
        return null;
    }
    public async Task<List<ArpUser>?> SubordinateDetails(int userId)
    {
        using (var cmd = MyCommand.CmdProc("SubordinateDetails", connectionString))
        {
            cmd.Parameters.AddWithValue("@userId", userId);
            using (var con = cmd.Connection)
            {
                await con.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                var json = await MyCommand.GetJson(reader);
                if (!string.IsNullOrWhiteSpace(json.ToString()))
                {
                    return JsonSerializer.Deserialize<List<ArpUser>>(json);
                }
            }
        }
        return null;
    }
    readonly IConfiguration _config;
    string connectionString;
    public MyDb(IConfiguration configuration)
    {
        _config = configuration;
        connectionString = _config["AppSettings:ConnectionString"]!;
    }

   
}
public interface IMyDb
{
    //Task<User?> Login(LoginData? login);
 
    Task<ArpUser?> GetOracleUser(int idNo);
    Task UpdateLoginHistory(int loginType, string machineName, string loginId, int statusId);
    //Task<User?> LoginByADInfo(ADUserInfo user);
    Task UpdateArpUser(ArpUser user);
    Task<ArpUser?> GetArpUserTreeByUser(int userId);
    Task<List<string>> GetIds();
    Task<ArpUser?> Login(int userId, string password);
    Task<List<AssetDelivery>?> AssetDeliveriesByBeneficiary(int userId, int beneficieryId);
    Task<List<ArpUser>?> SubordinateDetails(int userId);
}
