<h1>Image contrast editor</h1>
<p>Windows desktop application which allows to change contrast of any photo.
</p>
<h2>Table of contents</h2>
<ul>
  <li>
    <a href="#generalInfo">General info</a>
  </li>
   <li>
    <a href="#images">Images</a>
  </li>
  <li>
    <a href="#howToUse">How to use</a>
  </li>
  <li>
    <a href="#technologies">Technologies used</a>
  </li>
   <li>
    <a href="#environment">Programming environment</a>
  </li>
  <li>
    <a href="#status">Project status</a>
  </li>
  </ul>
  <h2 id="generalInfo">General info</h2>
  <p>The goal of this application was to implement an alghoritm, which changes image contrast, in both c# and asm and compares their execution times.
So as to examine alghoritms execution times a comparison was made between images of diffrent quality. Each alghoritm (asm, c#, optimized c#) 
  performed image contrast processing on the same photos using the same contrast factor value. Immediately, it turned out that the fastest alghoritm was
  the one written in assembly. Alghoritm implemented in c# with Microsoft optimization turned on was almost two times slower. Shockingly, when optimization 
  was turned off, it took almost 10 times more time for c# alghoritm to perform the same image processing compared to asm.
  Detailed results of the comparison are below in images section.
</p>
    <h2 id="images">Images</h2>
    <img src="https://user-images.githubusercontent.com/56251920/154487503-e56aeba3-a922-4529-a76c-1de32f60a739.png"></img>
    <img src="https://user-images.githubusercontent.com/56251920/154487805-9b95b964-2027-4510-a184-93e61197c772.png"></img>
   <h2 id="howToUse">How to use</h2>
  <ol>
  <li>
   Start the program.
  </li>
   <li>
   Choose which implementation of image contrast changing alghoritm you desire (by default its asm).
  </li>
  <li>
   Choose an image which contrast you want to change.
  </li>
  <li>
    Choose value of contrast factor.
  </li>
   <li>
    Click calculate button.
  </li>
   <li>
   Optionally, you can save your photo.
  </li>
  </ol>
  <h2 id="technologies">Technologies used</h2> 
 <ul>
  <li>
   c#
  </li>
  <li>
    asm
  </li>
   <li>
   xml
  </li>
  </ul>
   <h2 id="environment">Programming environment</h2> 
    <ul>
  <li>
   Visual studio
  </li>
  </ul>
    <h2 id="status">Project status</h2> 
    <p>Finished</p>
  
  
  
