﻿using System;
using System.Collections.Generic;

namespace Eudora.Api.Utils
{
    public class NameGenerator
    {
        private readonly IList<string> _firstNames = new List<string> { "ABIGAIL", "ADELAIDE", "ADRIANA", "ADRIENNE", "ALBERTA", "ALCIONE", "ALEXANDRA", "ALICE", "ALÍCIA", "ALINE", "AMANDA", "AMÉLIA", "ANA", "ANABELA", "ANASTÁCIA", "ANDREA", "ANDRESA", "ÂNGELA", "ANGÉLICA", "ANGELINA", "ANITA", "ANTÔNIA", "APARECIDA", "ARIADNE", "AUGUSTA", "BÁRBARA", "BEATRICE", "BEATRIZ", "BERENICE", "BERNADETE", "BETÂNIA", "BIANCA", "BRENDA", "BRUNA", "CAMILA", "CAMILLE", "CARMEM", "CAROLINA", "CASSANDRA", "CÁSSIA", "CATARINA", "CECÍLIA", "CÉLIA", "CÍNTIA", "CLARA", "CLARICE", "CLARISSA", "CLÁUDIA", "DANIELA", "DANIELLE", "DÉBORA", "DENISE", "ELEN", "ELENA", "ELIANA", "ELISA", "ELISABETE", "FABIANA", "FÁTIMA", "FERNANDA", "FLÁVIA", "GABRIELA", "ISABEL", "ISABELA", "ISADORA", "ISAURA", "JASMIM", "JÉSSICA", "JOANA", "JÚLIA", "JULIANA", "KARIN", "KARINA", "LARISSA", "LEILA", "LETÍCIA", "LÍVIA", "LÚCIA.", "LUCIANA", "LUDMILA", "LUÍSA", "MARA", "MARCELA", "MÁRCIA", "MARIA", "MARÍLIA", "MARINA", "MARISA", "MAYA", "MELISSA", "MICHELE", "MILENA", "MIRANDA", "MONALISA", "MÔNICA", "MORGANA", "NAIARA", "NARA", "NATÁLIA", "NATASHA", "PAOLA", "PATRÍCIA", "PAULA", "PRISCILA", "RAFAELA", "RAQUEL", "ROBERTA", "ROSA", "ROSANA", "RUTE", "SABRINA", "SAMARA", "SANDRA", "SANDY", "SARA", "SILVIA", "SÔNIA", "SOPHIA", "SORAIA", "STEPHANIE", "SUSANA", "TÁBATA", "TAÍS", "TALITA", "TEREZA", "ÚRSULA", "VALENTINA", "VALÉRIA", "VALQUÍRIA", "VERÔNICA", "VILMA", "VIRGÍNIA", "VIVIANA", "VIVIANE", "YASMIN", "YEDA", "YOLANDA	", "ZÉLIA", "ZULEICA", "ZULMIRA" };
        private readonly IList<string> _lastNames = new List<string> { "AGOSTINHO", "AGUIAR", "ALBUQUERQUE", "ALEGRIA", "ALENCASTRO", "ALMADA", "ALVES", "ALVIM", "AMORIM", "ANDRADE", "ANTUNES", "APARÍCIO", "ARAÚJO", "ARRUDA", "ASSIS", "ASSUNÇÃO", "BAPTISTA", "BARRETO", "BARROS", "BEIRA-MAR", "BELCHIOR", "BELÉM", "BERNARDES", "BITTENCOURT", "BOAVENTURA", "CAETANO", "CALIXTO", "CAMACHO", "CAMILO", "CAPELO", "CASTRO", "CAVALCANTE", "CHAVES", "CONCEIÇÃO", "CORTEREAL", "CORTÊS", "COUTINHO", "CRESPO", "CUNHA", "DANTAS", "DIAS", "DOMINGUES", "DORNELES", "DOSREIS", "DRUMOND", "FARIAS", "FERRARI", "FIGUEIREDO", "FIGUEIROA", "FLORES", "FOGAÇA", "FREITAS", "FURTADO", "GALVÃO", "GARCIA", "GASPAR", "GENTIL", "GERALDES", "GIL", "GODINHO", "GOMES", "GONZAGA", "GOULART", "GOUVEIA", "GUEDES", "GUIMARÃES", "JESUS", "JORDÃO", "LACERDA", "LESSA", "MACHADO", "MACIEL", "MAGALHÃES", "MAIA", "MALDONADO", "MARINHO", "MARQUES", "MARTINS", "MEDEIROS", "MEIRELES", "MELLO", "MENDES", "MENEZES", "MESQUITA", "MONTEIRO", "MORAIS", "MOREIRA", "MORGADO", "MOURA", "MUNIZ", "NEVES", "NOGUEIRA", "NOVAIS", "NÓBREGA", "ORNÉLAS", "OLIVEIRA", "OURIQUE", "PACHECO", "PADILHA", "PAIVA", "PARAÍSO", "PARIS", "PEIXOTO", "PERES", "PIMENTA", "PINHEIRO", "PORTELA", "QUARESMA", "QUARTEIRA", "QUEIROZ", "RAMIRES", "RAMOS", "RESENDE", "RIBEIRO", "SALAZAR", "SALES", "SALGADO", "SALGUEIRO", "SAMPAIO", "SANCHES", "SANTANA", "SIQUEIRA", "SOARES", "TAVARES", "TAVEIRA", "TEIXEIRA", "TELES", "TORRES", "TRINDADE", "VARELA", "VARGAS", "VASCONCELOS", "VASQUES", "VEIGA", "VELOSO", "VIDAL", "VIEIRA" };

        public string Create()
        {
            var random = new Random();

            var firstName = random.Next(0, _firstNames.Count);
            var lastName1 = random.Next(0, _lastNames.Count);
            var lastName2 = random.Next(0, _lastNames.Count);

            return _firstNames[firstName] + " " + _lastNames[lastName1] + " " + _lastNames[lastName2];
        }
    }
}