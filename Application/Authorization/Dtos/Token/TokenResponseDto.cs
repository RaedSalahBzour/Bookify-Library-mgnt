﻿using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Dtos.Token;

public class TokenResponseDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
