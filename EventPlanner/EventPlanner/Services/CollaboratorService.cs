using EventPlanner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EventPlanner.Services
{
    class CollaboratorService
    {
        private readonly string PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\collaborators.json");
        private static CollaboratorService singleton = null;


        public static CollaboratorService Singleton()
        {
            return singleton ??= new CollaboratorService();
        }

        public Collaborator GetCollaboratorInfo(int id)
        {
            return GetCollaborators().FirstOrDefault(collaborator => collaborator.ID == id);
        }

        public List<Collaborator> GetCollaborators()
        {
            List<Collaborator> collaborators = new List<Collaborator>();
            using (StreamReader reader = new StreamReader(PATH))
            {
                string data = reader.ReadToEnd();
                collaborators = JsonConvert.DeserializeObject<List<Collaborator>>(data);
            }
            return collaborators;
        }

        public List<Collaborator> Delete(Collaborator collaborator)
        {
            List<Collaborator> collaborators = GetCollaborators();
            collaborators.RemoveAll(el => el.ID == collaborator.ID);
            save(collaborators);
            return collaborators;
        }

        public List<Collaborator> Modify(Collaborator collaborator)
        {
            List<Collaborator> collaborators = GetCollaborators();
            for(int i = 0; i < collaborators.Count(); i++)
            {
                if(collaborators[i].ID == collaborator.ID)
                {
                    collaborators[i] = collaborator;
                }
            }
            save(collaborators);
            return collaborators;
        }

        public List<Collaborator> Add(Collaborator collaborator)
        {
            List<Collaborator> collaborators = GetCollaborators();
            collaborators.Add(collaborator);
            save(collaborators);
            return collaborators;
        }

        public int GetLastId()
        {
            List<Collaborator> collaborators = GetCollaborators();
            int greatest = 1;
            foreach (var collaborator in collaborators)
            {
                if(greatest < collaborator.ID)
                {
                    greatest = collaborator.ID;
                }
            }
            return ++greatest;
        }

        public void save(List<Collaborator> collaborators)
        {
            using (StreamWriter writer = new StreamWriter(PATH))
            {
                string data = JsonConvert.SerializeObject(collaborators);
                writer.WriteLine(data);
            }
        }
    }
}
