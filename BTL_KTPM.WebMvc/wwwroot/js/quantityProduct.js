const quantity = document.querySelector(".Quantity_Product");
const Add = document.querySelector(".Add_Quantity_Product");
const Minus = document.querySelector(".Minsus_Quantity_Product");
var Value = quantity.innerHTML;
console.log(Value);
Add.addEventListener("click",(e)=>{
    Value++;
    quantity.innerHTML = Value;
})
Minus.addEventListener("click",(e)=>{
    if(Value == 1){
        Value = 1
    }
    else{
        Value--;
    }
    
    quantity.innerHTML = Value;
})
