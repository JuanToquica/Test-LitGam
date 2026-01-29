# Test-LitGam

En la carpeta Data se encuentran los scriptable objects para la configuracion del juego
Dance library: guarda las animaciones de baile, en caso de añadir mas animaciones solo basta con agregar el nombre y el clip, el animator y los botones de UI se estableceran automaticamente al iniciar el juego
Player movement config: Guarda las preferencias de movimiento del jugador

En la carpeta Data/Weapons se encuentran los scriptable objects con la configuracion de los 3 tipos de arma
La tercer arma consiste en un projectile, el cual durante su trayectoria atrae a los objectos cercanos y los dirige hacia el punto de impacto, al impactar el proyectile los objetos atraidos seran repelidos como en una explosion