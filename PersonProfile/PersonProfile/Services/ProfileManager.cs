using Microsoft.Extensions.Logging;
using PersonProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonProfile.Services
{
    public class ProfileManager : IRepo<Profile>
    {
        private ProfileContext _context;
        private ILogger<ProfileManager> _logger;

        public ProfileManager(ProfileContext context, ILogger<ProfileManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Profile t)
        {
            try
            {
                _context.Profiles.Add(t);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
            }
        }

        public void Delete(Profile t)
        {
            try
            {
                _context.Profiles.Remove(t);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
            }
        }

        public Profile Get(int id)
        {
            try
            {
                Profile profile = _context.Profiles.FirstOrDefault(a => a.Id == id);
                return profile;
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
            }
            return null;
        }

        public IEnumerable<Profile> GetAll()
        {
            try
            {
                if (_context.Profiles.Count() == 0)
                    return null;
                return _context.Profiles.ToList();

            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
            }
            return null;
        }

        public void Update(int id, Profile t)
        {
            Profile profile = Get(id);
            if (profile != null)
            {
                profile.Name = t.Name;
                profile.Age = t.Age;
                profile.Qualification = t.Qualification;
                profile.IsEmployed = t.IsEmployed;
                profile.NoticePeriod = t.NoticePeriod;
                profile.CurrentCTC = t.CurrentCTC;

            }
            _context.SaveChanges();
        }
    }
}