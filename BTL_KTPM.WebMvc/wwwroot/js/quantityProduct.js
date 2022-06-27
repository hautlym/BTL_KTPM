const quantity = document.getElementById("Quantity_Product");
const Add = document.querySelector(".Add_Quantity_Product");
const Minus = document.querySelector(".Minsus_Quantity_Product");
var kq = quantity.value
console.log(kq);
console.log([quantity]);
Add.addEventListener("click", (e) => {
    e.preventDefault
    kq++;
    quantity.value=kq;
})
Minus.addEventListener("click", (e) => {
    e.preventDefault
   if(kq==1){
    kq=1
   }
   else{
    kq--;
   }
   quantity.value=kq;
})
