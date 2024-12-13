using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{ 
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase 
    {
         private readonly UserManager<AppUser> _UserManager;

         private readonly ITokenService _tokenService;

         private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager , ITokenService tokenService , SignInManager<AppUser> signInManager)
        {
            _UserManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser{
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    
                };
                if(registerDto.Password == null)
                {
                    return NotFound("please enter the password.");
                }

                var createdUser  = await _UserManager.CreateAsync(appUser , registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _UserManager.AddToRoleAsync(appUser , "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else {
                        return StatusCode(500 , roleResult.Errors);
                    }
                }
                else{
                    return StatusCode(500 , createdUser.Errors);
                }

            }
            catch(Exception e)
            {
             return StatusCode(500 , e);  
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
              if(!ModelState.IsValid)
                    return BadRequest(ModelState);

            var user = await _UserManager.Users.FirstOrDefaultAsync(c => c.UserName == loginDto.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid Username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user ,loginDto.Password , false);

            if(!result.Succeeded)
            {
                return Unauthorized("user name not found / or password incorrect");
            }
            return Ok(
                new NewUserDto
                {
                    UserName =user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

    }
}