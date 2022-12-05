grammar busquedaJson;

program : input+;

input: TEXTO PUNTO? | TEXTO array PUNTO?;

array: DOBLEPUNTO '['NUMERO']';


TEXTO: [A-Za-z_]+;
NUMERO: [0-9]+;

PUNTO : '.';
DOBLEPUNTO : ':';

WS: [ \t\n\r] + -> skip;