using System;
using System.Collections.Generic;
using System.Numerics;
using DungeonBS.Abilities;


namespace DungeonBS.Models { 
    public class Jugadores
{
    // Atributos básicos de jugador
    public string Nick { get; private set; }
    public int Vidas { get; private set; }
    public int Lvl { get; private set; }
    public int Exp { get; private set; }
    public int ExpNextLvl { get; private set; }
    public int Salud { get; private set; }
    public int MaxSalud{get; private set;}
    public bool Estado { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; private set; }
    public int Damage {get; set; }
    public int MagicDamage{get; set; }
    public int Armor { get; private set; }
    public int MagicResistance { get; private set; }
    public int Gold { get; private set; }
    
    // Atributos dinámicos y editables por el jugador, inventario y equipamiento.
    public List<Items> Inventario { get; set; }

    public List<Skills> Habilidades { get;  set; }
    public Dictionary<string, Items> Equipamiento { get; set; }

    public Jugadores(string nombre)
    {
        //Atributos basicos de identidad
        Nick = nombre;
        Vidas = 3;
        MaxSalud = 100;
        Salud= MaxSalud;
        Lvl = 0;
        Exp = 0;
        ExpNextLvl = 100;
        Estado = true;
        //Atributos de Combate
        Damage = 10;
        MagicDamage = 5;
        MaxMana = 100;
        Mana = MaxMana;
        Armor = 15;
        MagicResistance= 5;
        //Atributos Dinamicos Inventario, habilidades ,equipamiento y efectos
        Inventario = new List<Items>();
        Equipamiento = new Dictionary<string, Items>
        {
            {"Armadura", null},
            {"Espada", null},
            {"Escudo", null}
        };
    }

    // Métodos de jugador...
        /// Metodos
        public void SubirEXP(int Exp){
            if (Exp <= 0) return;

            Console.WriteLine($"\n ->[{this.Exp+Exp}/{this.ExpNextLvl}]]{Nick} + [{Exp}]Xp. ]");

            this.Exp += Exp;
            while (this.Exp >= this.ExpNextLvl)
            {
                this.Exp -= this.ExpNextLvl;
                // Subida de nivel
                Lvl += 1;
                this.Damage += 2;

                Console.WriteLine($"\n -> El jugador: {Nick} subió a nivel {Lvl}.");

                // Desbloqueos de habilidades con el nivel específico
                if (Lvl == 3)
                {
                    Console.WriteLine($"-> Felicidades {Nick}, alcanzaste el nivel 5 y desbloqueaste la habilidad 'Golpe Crítico'.");
                    Skills critico = new GolpeCritico();
                    Habilidades.Add(critico);
                    // Aquí agregar el llamado al método de añadir habilidad.
                }
                if (Lvl == 5)
                {
                    Console.WriteLine($"-> Felicidades {Nick}, alcanzaste el nivel 10 y desbloqueaste la habilidad 'Coraje'.");
                    Skills coraje = new Coraje();
                    Habilidades.Add(coraje);
                    // Aquí agregar el llamado al método de añadir habilidad.
                }
                if (Lvl == 8)
                {
                    Console.WriteLine($"-> Felicidades {Nick}, alcanzaste el nivel 10 y desbloqueaste la habilidad 'Coraje'.");
                    Skills coraje = new BolaDeFuego();
                    Habilidades.Add(coraje);
                    // Aquí agregar el llamado al método de añadir habilidad.
                }

                // Subida de atributos cada 5 niveles
                if (Lvl % 5 == 0)
                {
                    this.Armor += 2;
                    this.MagicResistance += 2;
                    this.MagicDamage += 1;

                    int temp = this.Mana;
                    this.MaxMana += 10;
                    Mana = (this.MaxMana - temp);
                }

                ExpNextLvl = (3 * ExpNextLvl) / 2;
            }
//Se quito por molestia al jugar :)
            //Console.WriteLine($"-> El jugador {Nick} tiene [{this.Exp}] pts de experiencia.");
        }
        public void GanarSalud(int Salud){
            if (Salud <= 0) return;
            Console.WriteLine($"\n !!! -> {Nick} + [{Salud}] ♥].");
            if (this.Salud + Salud >= MaxSalud){
                this.Salud = MaxSalud;
            }else{
                this.Salud += Salud;
            }
        }
        public void Atacar(Monsters Objetivo){
            if(Objetivo == null){
                Console.WriteLine("\n !!! -> No existe el objetivo.");
                return;
            }
            Items Espada;
            //Buscar espada en el diccionario de equipamiento, si no se encuentra se establece como null
            try {
                Espada = Equipamiento["Espada"];
                //Console.WriteLine($"Encontrado: {item.Name}, Valor: {item.Value}");
            } catch (KeyNotFoundException) {
                Espada = null;
                //Console.WriteLine("El objeto no se encuentra en el diccionario.");
            } catch (NullReferenceException){
                Espada = null;    //
            }

            if (Espada is Sword sword)
            {
                Objetivo.RecibirDmg(Damage + sword.Dmg, this);
            }
            else
            {
                Objetivo.RecibirDmg(Damage, this);
            }

            //La del proceso Objetivo.RecibirDmg((Lvl+1)*20, this);
        }
        public void PerderSalud(int dmg){
            if (dmg <= 0){ 
                Console.WriteLine("-> Parece que no hizo ni cosquillas... ");
                return;}
            int newDmg;
            if (this.Equipamiento["Escudo"] is Shield shield && shield != null){
                 newDmg= (int)(dmg / (1 + ((shield.Armoring+Armor) / 100.0)));
            }else{
                newDmg = (int)(dmg / (1 + (Armor / 100.0)));
            }
            Console.WriteLine("\n !!! -> " + Nick + " recibió [" + newDmg + "] de daño");
            if (newDmg >= Salud){
                Console.WriteLine("\n !!! -> " + Nick + " murió.");
                PerderVida();
                Salud = 0;
            }else{
                Salud -= newDmg;
            }
        }
        public void PerderVida(){
            Vidas -= 1;
            if(Vidas <= 0){
                Console.WriteLine("\n !!! -> "+Nick+" Murio definitivamente, ni los dioses podran resucitarlo.");
                Estado = false;
                Salud = 0;
            }else if(Vidas == 1){
                Estado= true;
                Salud = MaxSalud;
                Console.WriteLine("\n !!! -> "+Nick+" ha perdido una vida, pero aun puede seguir luchando una ultima vez.");
            }else if(Vidas > 1){
                Estado= true;
                Salud = MaxSalud;
                Console.WriteLine("\n !!! -> "+Nick+" ha perdido una vida, pero aun le quedan "+Vidas+" vidas.");
            }
        }
    
        public void GanarVidas(int vidas){
            Vidas += vidas;
            Console.WriteLine("\n !!! -> "+Nick+" ha ganado "+vidas+" vidas.");

            Console.WriteLine("\n !!! -> Presiona cualquier tecla para continuar.");
            Console.ReadKey();
        }

        public void GanarOro(int oro){
            this.Gold += oro;
            Console.WriteLine("\n !!! -> "+Nick+$" + {oro} de oro.");
        }
    public void UsarHabilidad(int indice, Monsters objetivo){
        if(Habilidades.Count == 0){
            Console.WriteLine("No tienes habilidades para usar.");
            return;
        }
        if (indice < 0 || indice >= Habilidades.Count){
             Console.WriteLine("Índice de habilidad no válido."); return; 
            } 
            Skills habilidad = Habilidades[indice]; 
            habilidad.Usar(this, objetivo);
        }
    }
}

