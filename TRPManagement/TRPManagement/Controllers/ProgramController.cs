﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRPManagement.DTOs;
using TRPManagement.EF;
using TRPManagement.Models;

namespace TRPManagement.Controllers
{
    public class ProgramController : Controller
    {
        MidPracticeTaskEntities db = new MidPracticeTaskEntities();
        
        [HttpGet]
        public ActionResult Create(int channelId) //this one is for create from the channel list
        {   

            var channelName = (from c in db.Channels
                               where c.ChannelId == channelId
                               select c.ChannelName).FirstOrDefault();

            ViewBag.ChannelId = channelId;
            ViewBag.ChannelName = channelName;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProgramDTO programDTO) //this one is for create from the channel list
        {
            var program = ConvertDTO.Convert(programDTO);
            db.Programs.Add(program);
            db.SaveChanges();
            TempData["msg"] = "Program added successfully!";
            return RedirectToAction("List", "Channel");
        }

        [HttpGet]
        public ActionResult CreateProgram()
        {
            ViewBag.channels = db.Channels.ToList();
            return View(new Program());
        }

        [HttpPost]
        public ActionResult CreateProgram(ProgramDTO program)
        {
            db.Programs.Add(ConvertDTO.Convert(program));
            db.SaveChanges();
            TempData["msg"] = "Program added successfully!";
            return RedirectToAction("ProgramList");
        }
        [HttpGet]
        public ActionResult EditProgram(int id)
        {
            var program = db.Programs.Find(id);
            ViewBag.channels = ConvertDTO.Convert(db.Channels.ToList());
            ViewBag.Channel_Name = db.Channels.Find(program.ChannelId);
            return View(ConvertDTO.Convert(program));
        }

        [HttpPost]
        public ActionResult EditProgram(ProgramDTO programDTO)
        {
            var program = db.Programs.Find(programDTO.ProgramId);
            program.ProgramName = programDTO.ProgramName;
            program.TRPScore = programDTO.TRPScore;
            program.ChannelId = programDTO.ChannelId;
            program.AirTime = programDTO.AirTime;
            db.SaveChanges();
            TempData["msg"] = "Program updated successfully!";
            return RedirectToAction("ProgramList");
        }

        [HttpGet]
        public ActionResult DeleteProgram(int id)
        {
            var program = db.Programs.Find(id);
            ViewBag.Channel_Name = db.Channels.Find(program.ChannelId);
            return View(ConvertDTO.Convert(program));
        }

        [HttpPost]
        public ActionResult DeleteProgram(ProgramDTO program, string reply)
        {
            var program_find = db.Programs.Find(program.ProgramId);
            if (reply.Equals("Yes"))
            {
                db.Programs.Remove(program_find);
                db.SaveChanges();
                TempData["msg"] = "Program deleted successfully!";
                return RedirectToAction("ProgramList");
            }
            TempData["msg"] = "Program deleted unsuccessfull!";
            return RedirectToAction("ProgramList");
        }

        public ActionResult ProgramList()
        {
            var channel = db.Channels.ToList();
            var program = db.Programs.ToList();
            return View(channel);
        }
    }
}