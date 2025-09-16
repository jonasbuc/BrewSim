namespace BrewSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sim = new BrewerySimulation();
            sim.Start();
            Console.WriteLine("Simulation running. Press ENTER to exit.");
            Console.ReadLine();
            sim.Stop();
        }
    }
}
/*
BrewerySimulation – Programforklaring

Dette program simulerer en bryggeripipeline, hvor flasker behandles i flere trin: vask, fyldning, påsætning af låg og pakning i kasser. Hvert trin har et begrænset antal maskiner (tråde), og flaskerne flyttes gennem trinvise, trådsikre buffere.

Oversigt over klasser og deres samspil:

1. Bottle
   - Repræsenterer en enkelt flaske med et unikt Id.

2. BoundedBuffer<T>
   - En generisk, trådsikker buffer med fast kapacitet (producer-consumer mønster).
   - Bruges til at holde flasker mellem hvert trin i processen.
   - Maskiner (tråde) tilføjer og fjerner flasker fra disse buffere.

3. BrewerySimulation
   - Styrer hele simuleringen og opretter alle nødvendige tråde.
   - Opretter fire buffere:
     - washedBuffer: Flasker klar til at blive fyldt (kapacitet 6).
     - filledBuffer: Flasker klar til at blive toppet (kapacitet 6).
     - toppedBuffer: Flasker klar til at blive pakket (kapacitet 24).
     - boxedBuffer: Kasser med 24 flasker (kapacitet 10).
   - Start() opretter og starter tråde for hver maskintype:
     - 1 flaske-generator (BottleGenerator): Opretter flasker med tilfældige intervaller og lægger dem i washedBuffer.
     - 3 vaske-maskiner (WashingMachine): Tager flasker fra washedBuffer, vasker dem og lægger dem i filledBuffer.
     - 6 fylde-maskiner (FillingMachine): Tager flasker fra filledBuffer, fylder dem og lægger dem i toppedBuffer (kun hvis der er plads til topping).
     - 6 toppe-maskiner (ToppingMachine): Tager flasker fra toppedBuffer, topper dem og sender dem til BoxingCollector.
     - 2 pakke-maskiner (BoxingMachine): Tager kasser med 24 flasker fra boxedBuffer og "pakker" dem.
   - BoxingCollector: Samler flasker i grupper af 24 og lægger dem i boxedBuffer, når en kasse er fuld.

Trådene arbejder parallelt og synkroniseres via buffere og locks, så ingen buffer bliver overfyldt eller tømt forkert. Programmet kan stoppes sikkert med Stop().

Samspil:
- Flasker bevæger sig fra generator → vask → fyldning → topping → pakning, via buffere.
- Hver maskine arbejder uafhængigt, men er begrænset af bufferens kapacitet og tilgængelighed.
- Hele processen vises løbende i konsollen.

Program.cs starter og stopper simuleringen, alt andet styres af BrewerySimulation og de tilhørende klasser.
*/