using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{ 
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase 
    {
         private readonly UserManager<AppUser> _UserManager;

         private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager , ITokenService tokenService)
        {
            _UserManager = userManager;
            _tokenService = tokenService;
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
        // [HttpGet]
        //   public async Task<IActionResult> GetAll()
        //   {
        //     if(!ModelState.IsValid)
        //     return BadRequest(ModelState);

        //     // var appUsers = await _UserManager.GetUserAsync();
        //     // return Ok(appUsers);
        //   }
    }
}