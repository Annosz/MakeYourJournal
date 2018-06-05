using AutoMapper;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeYourJournal.DAL.Mapping
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Issue, IssueModel>()
                    .AfterMap((i, dto) =>
                        {
                            dto.ArticleCount = i.Articles.Count;
                            dto.Number = i.IssueDetails.Number;
                            dto.Volume = i.IssueDetails.Volume;
                            dto.Name = i.IssueDetails.Name;
                            dto.Deadline = i.IssueDetails.Deadline;
                            dto.Description = i.IssueDetails.Description;
                            dto.ExpectedPageCount = i.IssueDetails.ExpectedPageCount;
                            dto.CopyNumber = i.IssueDetails.CopyNumber;
                        });
                cfg.CreateMap<IssueModel, Issue>()
                    .AfterMap((dto, i) => i.IssueDetails = new IssueDetails
                    {
                        Number = dto.Number,
                        Volume = dto.Volume,
                        Name = dto.Name,
                        Deadline = dto.Deadline,
                        Description = dto.Description,
                        ExpectedPageCount = dto.ExpectedPageCount,
                        CopyNumber = dto.CopyNumber,
                    });

                cfg.CreateMap<Article, ArticleModel>()
                    .AfterMap((a, dto, ctx) =>
                        dto.Todos = a.Items.Where(i => i.GetType() == typeof(Todo)).Select(t => ctx.Mapper.Map<TodoModel>(t)))
                    .AfterMap((a, dto, ctx) =>
                        dto.Notes = a.Items.Where(i => i.GetType() == typeof(Note)).Select(t => ctx.Mapper.Map<NoteModel>(t)));
                cfg.CreateMap<ArticleModel, Article>();

                cfg.CreateMap<Todo, TodoModel>()
                    .AfterMap((t, dto) =>
                        dto.ArticleTitle = t.Article.Title);
                cfg.CreateMap<TodoModel, Todo>();

                cfg.CreateMap<Note, NoteModel>()
                    .AfterMap((n, dto) =>
                        dto.ArticleTitle = n.Article.Title);
                cfg.CreateMap<NoteModel, Note>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
