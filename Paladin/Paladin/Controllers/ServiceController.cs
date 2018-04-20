using AutoMapper;
using Paladin.Models;
using Paladin.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Paladin.Controllers
{
    public class ServiceController : PaladinController
    {
        private readonly PaladinDbContext _context;

        public ServiceController()
        {
            _context = new PaladinDbContext();
        }

        public ActionResult GetApplicantsForReminders()
        {
            var applicants = _context.Applicants.ToList();
            var vmApplicants = new List<ApplicantVM>();
            foreach(var app in applicants)
            {
                vmApplicants.Add(Mapper.Map<ApplicantVM>(app));
            }

            return Xml(vmApplicants);
        }

    }
}