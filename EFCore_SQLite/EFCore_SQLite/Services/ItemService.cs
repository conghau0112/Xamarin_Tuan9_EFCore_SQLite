﻿using EFCore_SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    class ItemService : IItemService
    {
        private DatabaseContext _dbContext = new DatabaseContext();
        public ItemService()
        {

        }
        public async Task<bool> AddItemAsync(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async  Task<bool> DeleteItemAsync(int id)
        {
            var studentResult = _dbContext.Items.Where(t => t.Id.Equals(id)).FirstOrDefault();
            if (studentResult != null)
            {
                _dbContext.Items.Remove(studentResult);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(_dbContext.Items.Where(t => t.Id.Equals(id)).FirstOrDefault());
        }

        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await Task.FromResult(_dbContext.Items.ToList());
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                var studentResult = _dbContext.Items.Where(t => t.Id.Equals(item.Id)).FirstOrDefault();
                if (studentResult != null)
                {
                    studentResult.Name = item.Name;
                    studentResult.Image = item.Image;
                    studentResult.Description = item.Description;

                    _dbContext.Items.Update(studentResult);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
                //_dbContext.Items.Remove(item);
                //await _dbContext.SaveChangesAsync();
                //return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
