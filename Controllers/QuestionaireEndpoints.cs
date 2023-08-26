using Microsoft.EntityFrameworkCore;
using simplequiz.Model;
namespace simplequiz.Controllers;

public static class QuestionaireEndpoints
{
    public static void MapQuestionaireEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Questionaire", async (QuestionContext db) =>
        {
            return await (
    from q in db.Questionaire
    where q.Topic == "AZ-900" && q.Module == "Module 3"
    orderby q.Id ascending 
    select q
        ).Take(1000) // limit the number of results
        .OrderByDescending(c => c.Id)
        .Include(c => c.Choices)
        .ToListAsync();
            // return await db.Questionaire.Include(c=>c.Choices).Where(c => c.Topic == "AZ-900").ToListAsync();
            //    .OrderBy(q => q.Id)
            //.Skip(9)
            //.Take(21)
        })
        .WithName("GetAllQuestionaires")
        .Produces<List<Questionaire>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Questionaire/{id}", async (int Id, QuestionContext db) =>
        {
            return await db.Questionaire.FindAsync(Id)
                is Questionaire model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetQuestionaireById")
        .Produces<Questionaire>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Questionaire/{id}", async (int Id, Questionaire questionaire, QuestionContext db) =>
        {
            var foundModel = await db.Questionaire.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(questionaire);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateQuestionaire")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Questionaire/", async (Questionaire questionaire, QuestionContext db) =>
        {
            questionaire.Topic = "AZ-900";
            db.Questionaire.Add(questionaire);
            await db.SaveChangesAsync();
            return Results.Created($"/Questionaires/{questionaire.Id}", questionaire);
        })
        .WithName("CreateQuestionaire")
        .Produces<Questionaire>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Questionaire/{id}", async (int Id, QuestionContext db) =>
        {
            if (await db.Questionaire.FindAsync(Id) is Questionaire questionaire)
            {
                db.Questionaire.Remove(questionaire);
                await db.SaveChangesAsync();
                return Results.Ok(questionaire);
            }

            return Results.NotFound();
        })
        .WithName("DeleteQuestionaire")
        .Produces<Questionaire>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
