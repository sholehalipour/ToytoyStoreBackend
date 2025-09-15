using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Members;
using ToytoyStoreBackend.Entities;

namespace ToytoyStoreBackend.Endpoints
{
    public static class MemberEndponits
    {
        public static void MapMemberEndPoints(this WebApplication app)
        {
            app.MapGet("members/List",
 async ([FromServices] LibraryDB db) =>
{
    var result = await db.Members.Select(b => new MemberListDto
    {
        Id = b.Guid,
        Name = b.Name,
        Family = b.Family,
        UserName = b.UserName,
    }).ToListAsync();
    return result;


});
            app.MapPost("members/Create",
             async (
                [FromServices] LibraryDB db,
                 [FromBody] MemberAddDto memberAddDto) =>
            {
                var member = new Member
                {
                    Name = !string.IsNullOrEmpty(memberAddDto.Name) ? memberAddDto.Name : "بدون نام",
                    Family = !string.IsNullOrEmpty(memberAddDto.Family) ? memberAddDto.Family : " بدون نام خانوادگی",
                    UserName = !string.IsNullOrEmpty(memberAddDto.UserName) ? memberAddDto.UserName : "بدون نام کاربری",
                    Password = !string.IsNullOrEmpty(memberAddDto.Password) ? memberAddDto.Password : "بدون رمز عبور",

                };
                await db.Members.AddAsync(member);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "user Created!"
                };

            });
            app.MapPut("members/Update/{guid}",
             async ([FromServices] LibraryDB db,
              [FromRoute] string guid,
               [FromBody] MemberUpdateDto memberUpdateDto) =>
            {
                var b = await db.Members.FirstOrDefaultAsync(m => m.Guid == guid);
                if (b == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,

                        Message = "Not Found usert!"
                    };
                }
                b.Name = memberUpdateDto.Name ?? "بدون نام";
                b.Family = memberUpdateDto.Family;
                b.UserName = memberUpdateDto.UserName;
                // b.Password = memberUpdateDto.Password;
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "user Updated!"
                };
            });
            app.MapDelete("members/Delete/{guid}",
             async ([FromServices] LibraryDB db,
              [FromRoute] string guid) =>
            {
                var member = await db.Members.FirstOrDefaultAsync(m => m.Guid == guid);
                if (member == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Message = "Not Found user"
                    };
                }
                db.Members.Remove(member);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "User Removed!"
                };
            });
        }
    }
}