﻿using System.Collections.Generic;
using SAA.Models;
using SAA.Services.impl;

namespace SAA.Services
{
    public class SubjectService : ISubjectService
    {
        private static readonly SubjectService _instance = new SubjectService(new PersistenceService());
        private readonly IPersistenceService _persistenceService;

        // Constructor privado para evitar instanciación externa
        private SubjectService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        // Propiedad estática para acceder a la instancia única
        public static SubjectService Instance => _instance;

        // Obtiene todas las materias almacenadas.
        public List<Subject>? GetAllSubjects()
        {
            return _persistenceService.GetAll<Subject>("subjects");
        }

        // Obtiene una materia por su ID.
        public Subject? GetSubjectById(int subjectId)
        {
            return _persistenceService.GetById<Subject>(subjectId, "subjects");
        }

        // Agrega una nueva materia o actualiza una existente.
        public void AddSubject(Subject subject)
        {
            _persistenceService.AddOrUpdate(subject, "subjects");
        }

        // Actualiza los datos de una materia existente.
        public void UpdateSubject(Subject subject)
        {
            _persistenceService.AddOrUpdate(subject, "subjects");
        }

        // Elimina una materia por su ID.
        public void DeleteSubject(int subjectId)
        {
            _persistenceService.Delete<Subject>(subjectId, "subjects");
        }
    }
}