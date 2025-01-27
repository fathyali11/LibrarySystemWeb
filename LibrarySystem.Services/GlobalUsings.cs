﻿global using AutoMapper;
global using LibrarySystem.Data.Data;
global using LibrarySystem.Data.Repository;
global using LibrarySystem.Domain.Abstractions;
global using LibrarySystem.Domain.Abstractions.Errors;
global using LibrarySystem.Domain.DTO.Books;
global using LibrarySystem.Domain.Entities;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Caching.Hybrid;
global using OneOf;
global using LibrarySystem.Domain.IRepository;
global using AutoMapper.QueryableExtensions;
global using LibrarySystem.Domain.DTO.ApplicationUsers;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using LibrarySystem.Domain.DTO.Author;
global using Microsoft.Extensions.Logging;
global using System.Text;
global using LibrarySystem.Domain.Abstractions.ConstValues;
global using LibrarySystem.Services.Services.Emails;
global using LibrarySystem.Services.Services.Tokens;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Microsoft.AspNetCore.WebUtilities;
global using System.Text.Json;
global using Microsoft.Extensions.Caching.Distributed;
global using LibrarySystem.Domain.DTO.Categories;
global using MailKit.Net.Smtp;
global using MailKit.Security;
global using Microsoft.Extensions.Options;
global using MimeKit;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using Microsoft.IdentityModel.Tokens;

global using LibrarySystem.Domain.Abstractions.Pagination;
global using LibrarySystem.Domain.DTO.Common;
global using System.Linq.Dynamic.Core;
global using Hangfire;
global using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
global using LibrarySystem.Domain.DTO.BorrowBooks;