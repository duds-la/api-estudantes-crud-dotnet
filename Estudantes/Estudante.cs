﻿namespace ApiCrud.Estudantes
{
    public class Estudante
    {
        public Guid Id { get; init; }

        //private set = alterações só dentro da 
        //classe, não conseguimos instanciar e abrir esse método
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public Estudante(string nome) 
        {
            Nome = nome;
            Id = Guid.NewGuid();
            Ativo = true;
        }
    }
}