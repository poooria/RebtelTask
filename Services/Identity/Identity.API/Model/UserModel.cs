﻿namespace Identity.API.Model;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }
    public string DisplayName { get; set; }
}