.checkbox {
  display: block;
  position: relative;
  cursor: pointer;
  height: 20px;
  width: 20px;
  margin-left: 15px;
  margin-top: 2px;
}

.checkbox input {
  position: absolute;
  opacity: 0;
  cursor: pointer;
  height: 0;
  width: 0;
}

.tick {
  position: absolute;
  top: 0;
  border: 1px solid #33333375;
  left: 0;
  height: 18px;
  width: 18px;
  border-radius: 3px;
  padding: 4px 5.5px;

}

.checkbox:hover input~.tick::before {
  opacity: 0.5;
}

.checkbox input:checked~.tick {
  background-color: #31b9e2;
}

.tick:after {
  content: "";
  position: absolute;
  display: none;
}

.checkbox input:checked~.tick:after {
  display: block;
}

.checkbox .tick:after {
  width: 5px;
  height: 8px;
  border: solid white;
  border-width: 0 2px 2px 0;
  top: 3px;
  -webkit-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  transform: rotate(45deg);
}